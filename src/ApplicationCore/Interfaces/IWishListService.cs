using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IWishListService
    {
        Task<int> GetWishListItemCountAsync(string userName);
        Task AddItemToWishList(int wishListId, int catalogItemId, decimal price, string priceSymbol, int quantity = 1);
        Task SetQuantities(int wishListId, Dictionary<string, int> quantities);
        Task SetNotifies(int wishListId, int catalogItemId, bool notifyCasePriceChanges);
        Task SetWishName(int wishListId, string wishName);
        Task DeleteWishListAsync(int wishListId);
    }
}
