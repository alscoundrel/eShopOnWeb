using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class CatalogFilterPaginatedSpecification : BaseSpecification<CatalogItem>
    {
        public CatalogFilterPaginatedSpecification(int skip, int take, string term, string orderBy, int? ordination, int? brandId, int? typeId)
            : base(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
                (!typeId.HasValue || i.CatalogTypeId == typeId) &&
                (string.IsNullOrEmpty(term) || i.Name.Contains(term))
            )
        {
            ApplyPaging(skip, take);
        }
    }
}
