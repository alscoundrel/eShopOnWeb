using System;
using System.Linq.Expressions;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class CatalogFilterPaginatedSpecification : CatalogFilterSpecification
    {
        public CatalogFilterPaginatedSpecification(int skip, int take, string term, string orderBy, int? ordination, int? brandId, int? typeId)
            : base(brandId, typeId, term)
        {
            ApplyPaging(skip, take);
            //ApplyOrderBy(i => i.Name);
            // if(!string.IsNullOrEmpty(orderBy)){
            //     Expression<Func<CatalogItem, object>> expr = x => x.GetType().GetProperty(orderBy).GetValue(x, null);
            //     ApplyOrderBy(expr);
            // }
        }
    }
}
