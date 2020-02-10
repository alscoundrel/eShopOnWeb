using System;
using ApplicationCore.UseTypes;

namespace Microsoft.eShopWeb.Web.Pages.WishList
{
    public class WishListItemViewModel
    {
        public int Id { get; set; }
        public int CatalogItemId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }
        public string PriceSymbol { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public DateTimeOffset WishDate { get; set;}
        public bool NotifyCasePriceChanges {get; set;}
        public bool NotifyWhenAvailable {get; set;}
        public MediaOptions NotifyChoice {get; set;}
    }
}
