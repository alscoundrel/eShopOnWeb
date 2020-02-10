using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using System.Linq;
using Ardalis.GuardClauses;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class WishListService : IWishListService
    {
        private readonly IAsyncRepository<WishList> _wishListRepository;
        private readonly IAppLogger<WishListService> _logger;

        public WishListService(IAsyncRepository<WishList> wishListRepository,
            IAppLogger<WishListService> logger)
        {
            _wishListRepository = wishListRepository;
            _logger = logger;
        }

        public async Task AddItemToWishList(int wishListId, int catalogItemId, decimal price, string priceSymbol, int quantity = 1)
        {
            var wishList = await _wishListRepository.GetByIdAsync(wishListId);

            wishList.AddItem(catalogItemId, price, priceSymbol, quantity);

            await _wishListRepository.UpdateAsync(wishList);
        }

        public async Task DeleteWishListAsync(int wishListId)
        {
            var wishList = await _wishListRepository.GetByIdAsync(wishListId);
            await _wishListRepository.DeleteAsync(wishList);
        }

        public Task<int> GetWishListItemCountAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetNotifies(int wishListId, int catalogItemId, bool notifyCasePriceChanges)
        {
            //Guard.Against.Null(notifyCasePriceChanges, nameof(notifyCasePriceChanges));
            var WishList = await _wishListRepository.GetByIdAsync(wishListId);
            var WishItem = WishList.Items.Where(i => i.CatalogItemId == catalogItemId ).FirstOrDefault();
            WishItem.NotifyCasePriceChanges = notifyCasePriceChanges;
            await _wishListRepository.UpdateAsync(WishList);
        }


        public Task SetQuantities(int wishListId, Dictionary<string, int> quantities)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetWishName(int wishListId, string wishName)
        {
            var WishList = await _wishListRepository.GetByIdAsync(wishListId);
            WishList.WishName = wishName;
            await _wishListRepository.UpdateAsync(WishList);
        }
    }
}
