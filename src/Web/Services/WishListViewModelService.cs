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

        public Task<WishListViewModel> GetOrCreateWishListForUser(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}
