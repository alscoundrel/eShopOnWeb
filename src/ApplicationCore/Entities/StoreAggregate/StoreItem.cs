using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.StoreAggregate
{
    public class StoreItem : IAggregateRoot
    {
        public int StoreId { get; set; }
        public int CatalogItemId { get; set; }
        public decimal Amount { get; set; }
        public string Unit { get; set; }
    }
}
