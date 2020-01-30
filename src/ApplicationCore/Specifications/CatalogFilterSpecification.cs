using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{

    public class CatalogFilterSpecification : BaseSpecification<CatalogItem>
    {
        public CatalogFilterSpecification(int? brandId, int? typeId, string term)
            : base(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
                (!typeId.HasValue || i.CatalogTypeId == typeId) &&
                (string.IsNullOrEmpty(term) || i.Name.Contains(term))
            )
        {
        }
    }
}
