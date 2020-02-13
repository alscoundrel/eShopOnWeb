using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.StoreAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public sealed class StoresItemsSpecification : BaseSpecification<StoreItem>
    {
        public StoresItemsSpecification(Store store)
            :base(b => b.StoreId == store.Id)
        {
        }
        public StoresItemsSpecification(CatalogItem catalogItem)
            :base(b => b.CatalogItemId == catalogItem.Id)
        {
        }
    }
}
