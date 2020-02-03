using Microsoft.eShopWeb.ApplicationCore.Exceptions;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Moq;
using System;
using System.Linq;
using Xunit;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.BasketServiceTests
{
    public class SetQuantities
    {
        private readonly int _invalidId = -1;
        private readonly Mock<IAsyncRepository<Basket>> _mockBasketRepo;

        public SetQuantities()
        {
            _mockBasketRepo = new Mock<IAsyncRepository<Basket>>();
        }

        [Fact]
        public async Task ThrowsGivenInvalidBasketId()
        {
            var basketService = new BasketService(_mockBasketRepo.Object, null);

            await Assert.ThrowsAsync<BasketNotFoundException>(async () =>
                await basketService.SetQuantities(_invalidId, new System.Collections.Generic.Dictionary<string, int>()));
        }

        [Fact]
        public async Task ThrowsGivenNullQuantities()
        {
            var basketService = new BasketService(null, null);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await basketService.SetQuantities(123, null));
        }


        [Fact]
        public async Task Update_ExistingItemQty_Succeeds() {
            var basketId = 10;
            var basket = new Basket();
            var itemId = 1;
            var inicialQty = 5;

            basket.AddItem(itemId, 10, 1);
            var targetItem = basket.Items.First();
            targetItem.Id = itemId;
            // targetItem.Id = itemId;
            _mockBasketRepo.Setup(
                x => x.GetByIdAsync(basketId)).ReturnsAsync(basket);

            var basketService = new BasketService(_mockBasketRepo.Object, null);
            var targetItemQty = inicialQty;
            var quantities = new System.Collections.Generic.Dictionary<string, int>() {
                { itemId.ToString(), targetItemQty }
            };
            await basketService.SetQuantities(
                    basketId,
                    quantities);
            Assert.Equal(targetItemQty, targetItem.Quantity);
            _mockBasketRepo.Verify(x => x.UpdateAsync(basket), Times.Once());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        public async Task SetQuantotytoZero_Removes_Item_from_bascket(int numInitialItemsBasket) {
            var random = new Random();
            var basketId = 10;
            var basket = new Basket();

            //CREATE INICIAL BASKET
            foreach (var itemId in Enumerable.Range(1,numInitialItemsBasket))
            {
                var inicialQty = random.Next(1,10);
                var itemPrice = random.Next(20,50);
                basket.AddItem(itemId, itemPrice, inicialQty);
            }
            foreach (var item in basket.Items)
            {
                item.Id = item.CatalogItemId;
            }
            // END INICIAL BASKET
            var inicialItemsCount = basket.Items.Count;

            // targetItem.Id = itemId;
            _mockBasketRepo.Setup(
                x => x.GetByIdAsync(basketId)).ReturnsAsync(basket);

            var basketService = new BasketService(_mockBasketRepo.Object, null);
            // BEGIN DECIDE ITEM TO REMOVE
            var itemIdToRemove = random.Next(1, numInitialItemsBasket);
            var itemToRemove = basket.Items.Where(item => item.Id == itemIdToRemove).First();
            var quantities = new System.Collections.Generic.Dictionary<string, int>();
            quantities.Add(itemToRemove.Id.ToString(), 0);
            var numItemsToRemove = quantities.Count;
            // END DECIDE ITEM TO REMOVE
            await basketService.SetQuantities(
                    basketId,
                    quantities);

            Assert.True(basket.Items.Count == inicialItemsCount - numItemsToRemove);
            _mockBasketRepo.Verify(x => x.UpdateAsync(basket), Times.Once());
        }

        [Theory]
        [InlineData(4, 1)]
        [InlineData(4, 2)]
        public async Task SetQuantotytoZero_Removes_Items_from_bascket(int numInitialItemsBasket, int numItemsToRemove=0) {
            if(numInitialItemsBasket < numItemsToRemove){
                throw new Exception("");
            }

            var random = new Random();
            var basketId = 10;
            var basket = new Basket();

            //CREATE INICIAL BASKET
            foreach (var itemId in Enumerable.Range(1,numInitialItemsBasket))
            {
                var inicialQty = random.Next(1,10);
                var itemPrice = random.Next(20,50);
                basket.AddItem(itemId, itemPrice, inicialQty);
            }
            foreach (var item in basket.Items)
            {
                item.Id = item.CatalogItemId;
            }
            // END INICIAL BASKET
            var inicialItemsCount = basket.Items.Count;

            // targetItem.Id = itemId;
            _mockBasketRepo.Setup(
                x => x.GetByIdAsync(basketId)).ReturnsAsync(basket);

            var basketService = new BasketService(_mockBasketRepo.Object, null);
            // BEGIN DECIDE ITEM TO REMOVE
            var quantities = new System.Collections.Generic.Dictionary<string, int>();
            foreach (var itemToRemove in basket.Items.Take(numItemsToRemove))
            {
                quantities.Add(itemToRemove.Id.ToString(), 0);
            }
            
            // END DECIDE ITEM TO REMOVE
            await basketService.SetQuantities(
                    basketId,
                    quantities);

            Assert.True(basket.Items.Count == inicialItemsCount - numItemsToRemove);
            _mockBasketRepo.Verify(x => x.UpdateAsync(basket), Times.Once());
        }

    }
}
