using System;
using ApplicationCore.UseTypes;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate
{
    public class WishItem : BaseEntity, IAggregateRoot
    {
        public string WishId { get; set; }
        public DateTimeOffset WishDate { get; private set; } = DateTimeOffset.Now;
        public bool NotifyCasePriceChanges {get; set;} = true;
        public bool NotifyWhenAvailable {get; set;} = true;
        public MediaOptions NotifyChoice {get; set;} = MediaOptions.EMail;
        public decimal UnitPrice { get; set; }
        public string PriceSymbol { get; set; }
        public int Quantity { get; set; }
        public int CatalogItemId { get; set; }
    }
}
