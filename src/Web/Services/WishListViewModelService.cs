using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.WishList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public class WishListViewModelService : IWishListViewModelService
    {
        private readonly IAsyncRepository<WishList> _wishListRepository;
        private readonly IUriComposer _uriComposer;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public WishListViewModelService(IAsyncRepository<WishList> wishListRepository,
            IAsyncRepository<CatalogItem> itemRepository,
            IUriComposer uriComposer)
        {
            _wishListRepository = wishListRepository;
            _uriComposer = uriComposer;
            _itemRepository = itemRepository;
        }

        public async Task<WishListViewModel> GetOrCreateWishListForUser(string userName)
        {
            var wishListSpec = new WishListWithItemsSpecification(userName);
            var wishList = (await _wishListRepository.ListAsync(wishListSpec)).FirstOrDefault();

            if (wishList == null)
            {
                return await CreateWishListForUser(userName);
            }
            return await CreateViewModelFromWishList(wishList);
        }

        public async Task<WishListViewModel> CreateWishListForUser(string userId){
            var wishList = new WishList() { WisherId = userId };
            await _wishListRepository.AddAsync(wishList);

            return new WishListViewModel()
            {
                WisherId = wishList.WisherId,
                Id = wishList.Id,
                Items = new List<WishListItemViewModel>()
            };
        }

        public async Task<WishListViewModel> CreateViewModelFromWishList(WishList wishList){
            var viewModel = new WishListViewModel();
            viewModel.Id = wishList.Id;
            viewModel.WisherId = wishList.WisherId;
            viewModel.WishName = wishList.WishName;
            viewModel.Items = await GetWishListItems(wishList.Items); ;
            return viewModel;
        }

        private async Task<List<WishListItemViewModel>> GetWishListItems(IReadOnlyCollection<WishItem> wishItems)
        {
            var items = new List<WishListItemViewModel>();
            foreach (var item in wishItems)
            {
                var itemModel = new WishListItemViewModel
                {
                    Id = item.Id,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    CatalogItemId = item.CatalogItemId,
                    PriceSymbol = item.PriceSymbol,
                    NotifyCasePriceChanges = item.NotifyCasePriceChanges,
                    NotifyWhenAvailable = item.NotifyWhenAvailable,
                    NotifyChoice = item.NotifyChoice,
                    WishDate = item.WishDate

                };
                var catalogItem = await _itemRepository.GetByIdAsync(item.CatalogItemId);
                itemModel.PictureUrl = _uriComposer.ComposePicUri(catalogItem.PictureUri);
                itemModel.ProductName = catalogItem.Name;
                items.Add(itemModel);
            }

            return items;
        }
    }
}
