using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class CatalogFilterPaginatedSpecification : CatalogFilterSpecification
    {
        public CatalogFilterPaginatedSpecification(int skip, int take, string term, string orderBy, int? ordination, int? brandId, int? typeId)
            : base(brandId, typeId, term)
        {
            ApplyPaging(skip, take);
        }
    }
}
