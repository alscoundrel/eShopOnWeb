using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public class CatalogItemViewModelService : ICatalogItemViewModelService
    {
        private readonly IAsyncRepository<CatalogItem> _catalogItemRepository;

        public CatalogItemViewModelService(IAsyncRepository<CatalogItem> catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }

        public async Task UpdateCatalogItem(CatalogItemViewModel viewModel)
        {
            //Get existing CatalogItem
            var existingCatalogItem = await _catalogItemRepository.GetByIdAsync(viewModel.Id);

            //Build updated CatalogItem
            var updatedCatalogItem = existingCatalogItem;
            updatedCatalogItem.Name = viewModel.Name;
            updatedCatalogItem.Price = viewModel.Price;
            updatedCatalogItem.ShowPrice = viewModel.ShowPrice;

            await _catalogItemRepository.UpdateAsync(updatedCatalogItem);
        }

        public async Task AddCatalogItem(CatalogItemViewModel viewModel)
        {
            //Build updated CatalogItem
            var addCatalogItem = new CatalogItem();
            addCatalogItem.Description = viewModel.Name;
            addCatalogItem.Name = viewModel.Name;
            addCatalogItem.PictureUri = viewModel.PictureUri;
            addCatalogItem.Price = viewModel.Price;
            addCatalogItem.ShowPrice = viewModel.ShowPrice;

            await _catalogItemRepository.AddAsync(addCatalogItem);
        }
    }
}
