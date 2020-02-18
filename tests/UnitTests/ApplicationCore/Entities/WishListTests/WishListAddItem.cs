
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;
using System;
using System.Linq;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.WishlistTests
{
    public class WishlistAddItem
    {
        private int _testCatalogItemId = 21;
        private decimal _testUnitPrice = 6.54m;
        private int _testQuantity = 5;
        private string _priceSymbol = "€";

        [Fact]
        public void AddsWishListItemIfNotPresent()
        {
            var wishList = new WishList();
            wishList.AddItem(_testCatalogItemId, _testUnitPrice, _priceSymbol, _testQuantity);

            var firstItem = wishList.Items.Single();
            Assert.Equal(_testCatalogItemId, firstItem.CatalogItemId);
            Assert.Equal(_testUnitPrice, firstItem.UnitPrice);
            Assert.Equal(_testQuantity, firstItem.Quantity);
        }

        [Fact]
        public void IncrementsQuantityOfItemIfPresent()
        {
            var wishList = new WishList();
            wishList.AddItem(_testCatalogItemId, _testUnitPrice, _priceSymbol, _testQuantity);
            wishList.AddItem(_testCatalogItemId, _testUnitPrice, _priceSymbol, _testQuantity);

            var firstItem = wishList.Items.Single();
            Assert.Equal(_testQuantity * 2, firstItem.Quantity);
        }
        
        [Fact]
        public void KeepsOriginalUnitPriceIfMoreItemsAdded()
        {
            var wishList = new WishList();
            wishList.AddItem(_testCatalogItemId, _testUnitPrice, _priceSymbol, _testQuantity);
            wishList.AddItem(_testCatalogItemId, _testUnitPrice * 2, _priceSymbol, _testQuantity);

            var firstItem = wishList.Items.Single();
            Assert.Equal(_testUnitPrice, firstItem.UnitPrice);
        }

        [Fact]
        public void DefaultsToQuantityOfOne()
        {
            var wishList = new WishList();
            wishList.AddItem(_testCatalogItemId, _testUnitPrice, _priceSymbol);

            var firstItem = wishList.Items.Single();
            Assert.Equal(1, firstItem.Quantity);
        }

        [Fact]
        public void RemoveItemPassingCatalogItemId(){
            var random = new Random();
            var numberOfItems = random.Next(1,10);
            var removeTheId = random.Next(_testCatalogItemId, _testCatalogItemId + numberOfItems - 1);

            var wishList = new WishList();
            for(var count = 0; count < numberOfItems; count++){
                wishList.AddItem(_testCatalogItemId + count, _testUnitPrice, _priceSymbol, _testQuantity);
            }
            
            wishList.RemoveItem(_testCatalogItemId);

            Assert.Equal(numberOfItems - 1, wishList.Items.Count);
        }

        [Fact]
        public void RemoveEmptyItems()
        {
            var wishList = new WishList();
            wishList.AddItem(_testCatalogItemId, _testUnitPrice, _priceSymbol, 0);
            wishList.RemoveEmptyItems();

            Assert.Equal(0, wishList.Items.Count);
        }

    }
}