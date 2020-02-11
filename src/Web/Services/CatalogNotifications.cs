using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    /// <summary>
    /// For Catalog Notifications
    /// </summary>
    public class CatalogNotifications
    {
        private readonly IAsyncRepository<CatalogItem> _itemRepository;
        private readonly IAsyncRepository<WishList> _wishListRepository;
        private readonly IEmailSender _emailSender;
        public CatalogNotifications(IAsyncRepository<CatalogItem> itemRepository, IAsyncRepository<WishList> wishListRepository, IEmailSender emailSender){
            _itemRepository = itemRepository;
            _wishListRepository = wishListRepository;
            _emailSender = emailSender;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="priceChenged"></param>
        /// <returns></returns>
        public async Task CatalogItemsNotifyAsync(int itemId, decimal priceNew){
            var catalogItem = await _itemRepository.GetByIdAsync(itemId);
            
            //All wishLists
            var wishesList = await _wishListRepository.ListAllAsync();
            foreach (var wishList in wishesList)
            {
                //items of wishList
                foreach (var item in wishList.Items)
                {
                    // if this item is in this wishList
                    if(item.Id == itemId){
                        
                        if(priceNew != catalogItem.Price){
                            await CatalogItemNotify(wishList.WisherId, catalogItem, priceNew);
                        }
                    }
                }
            }
        }

        public async Task CatalogItemNotify(string email, CatalogItem catalogItem, decimal priceNew){
            var subject = $"EShopOnWeb - {catalogItem.Name}";
            var message = "";

            await _emailSender.SendEmailAsync(email, subject, message);
        }



    }
}
