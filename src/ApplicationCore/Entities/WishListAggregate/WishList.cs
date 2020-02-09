using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.UseTypes;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate
{
    public class WishList : BaseEntity, IAggregateRoot
    {
        public string WisherId {get; set;}
        public string WishName {get; set;}
        private readonly List<WishItem> _items = new List<WishItem>();
        public IReadOnlyCollection<WishItem> Items => _items.AsReadOnly();

        public void AddItem(int catalogItemId, decimal unitPrice, int quantity = 1)
        {
            if (!Items.Any(i => i.CatalogItemId == catalogItemId))
            {
                _items.Add(new WishItem()
                {
                    WishId = this.WisherId,
                    CatalogItemId = catalogItemId,
                    Quantity = quantity,
                    UnitPrice = unitPrice
                });
                return;
            }
            var existingItem = Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);
            existingItem.Quantity += quantity;
        }

        public void RemoveItem(int catalogItemId){
            var existingItem = Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);
            _items.Remove(existingItem);
        }
        public void RemoveEmptyItems()
        {
            _items.RemoveAll(i => i.Quantity == 0);
        }
    }
}
