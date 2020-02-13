using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.StoreAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Store.Availability;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Web.Services
{
    public class StoreViewModelService : IStoreViewModelService
    {
        private readonly ILogger<StoreViewModelService> _logger;
        private readonly IAsyncRepository<Store> _storeRepository;
        private readonly IAsyncRepository<CatalogItem> _catalogItemRepository;
        private readonly CatalogContext _repository;

        public StoreViewModelService(CatalogContext repository, IAsyncRepository<Store> storeRepository, IAsyncRepository<CatalogItem> catalogItemRepository, ILoggerFactory loggerFactory){
            _repository = repository;
            _storeRepository = storeRepository;
            _catalogItemRepository = catalogItemRepository;
            _logger = loggerFactory.CreateLogger<StoreViewModelService>();
        }
        public async Task<StoreItemsAvailabilityViewModel> GetItemAvailabilityByStore(int catalogItemId, CancellationToken cancelationToken = default)
        {
            var catalogItem = await _catalogItemRepository.GetByIdAsync(catalogItemId);
            cancelationToken.ThrowIfCancellationRequested();
            if(catalogItem == null){
                _logger.LogError("Catalog Item not finded id {catalogItemId}", catalogItemId);
                throw new System.Exception($"Catalog Item not finded id {catalogItemId}");
            }

            var itemsAvailabulityByStore = _repository.StoresItems.Where(x => x.CatalogItemId == catalogItemId).ToList();
            cancelationToken.ThrowIfCancellationRequested();
            var stores = await this.GetStores(cancelationToken);
            
            var stokeItemsAvailabilityViewModel = new List<ItemsAvailabilityByStoreViewModel>();
            foreach (var stokes in itemsAvailabulityByStore)
            {
                stokeItemsAvailabilityViewModel.Add( new ItemsAvailabilityByStoreViewModel(){
                    Amount = stokes.Amount,
                    Unit = stokes.Unit,
                    StoreId = stokes.StoreId,
                    StoreName =  stores.Where(x => x.Value == stokes.StoreId.ToString()).FirstOrDefault().Text         
                });
            }

            var storeItemsAvailabilityViewModel = new StoreItemsAvailabilityViewModel(){
                Name = catalogItem.Name,
                CatalogItemId = catalogItemId,
                ItemsAvailabilityViewModels = stokeItemsAvailabilityViewModel
            };


            return storeItemsAvailabilityViewModel;
        }

        public async Task<IEnumerable<SelectListItem>> GetStores(CancellationToken cancelationToken = default)
        {
            _logger.LogInformation("GetStores called.");
            var stores = await _storeRepository.ListAllAsync();
            cancelationToken.ThrowIfCancellationRequested();
            
            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (Store store in stores)
            {
                items.Add(new SelectListItem() { Value = store.Id.ToString(), Text = store.Name });
            }

            return items;
        }
    }
}