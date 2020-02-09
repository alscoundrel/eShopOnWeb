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

        public Task AddItemToWishList(int wishListId, int catalogItemId, decimal price, int quantity = 1)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteWishListAsync(int wishListId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetWishListItemCountAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task SetQuantities(int wishListId, Dictionary<string, int> quantities)
        {
            throw new System.NotImplementedException();
        }
    }
}
