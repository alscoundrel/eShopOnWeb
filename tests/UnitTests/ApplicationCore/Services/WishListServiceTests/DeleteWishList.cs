using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Services.WishListServiceTests
{
    public class DeleteWishList
    {
        private Mock<IAsyncRepository<WishList>> _mockWishListRepo;

        public DeleteWishList()
        {
            _mockWishListRepo = new Mock<IAsyncRepository<WishList>>();
        }

        [Fact]
        public async Task Should_InvokeWishListRepositoryDeleteAsync_Once()
        {
            var wishList = new WishList();
            wishList.AddItem(1, It.IsAny<decimal>(), "€", It.IsAny<int>());
            wishList.AddItem(2, It.IsAny<decimal>(), "€", It.IsAny<int>());
            _mockWishListRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(wishList);
            var wishListService = new WishListService(_mockWishListRepo.Object, null);

            await wishListService.DeleteWishListAsync(It.IsAny<int>());

            _mockWishListRepo.Verify(x => x.DeleteAsync(It.IsAny<WishList>()), Times.Once);
        }
    }
}
