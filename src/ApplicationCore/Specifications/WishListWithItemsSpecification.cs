using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public sealed class WishListWithItemsSpecification : BaseSpecification<WishList>
    {
        public WishListWithItemsSpecification(int WishListId)
            :base(b => b.Id == WishListId)
        {
            AddInclude(b => b.Items);
        }
        public WishListWithItemsSpecification(string wisherId)
            :base(b => b.WisherId == wisherId)
        {
            AddInclude(b => b.Items);
        }
    }
}
