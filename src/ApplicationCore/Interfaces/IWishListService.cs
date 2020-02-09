using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IWishListService
    {
        Task<int> GetWishListItemCountAsync(string userName);
        Task AddItemToWishList(int wishListId, int catalogItemId, decimal price, int quantity = 1);
        Task SetQuantities(int wishListId, Dictionary<string, int> quantities);
        Task DeleteWishListAsync(int wishListId);
    }
}
