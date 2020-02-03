using Microsoft.eShopWeb.ApplicationCore.Entities;
using System;
using System.Linq.Expressions;


namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{

    public class CatalogFilterSpecification : BaseSpecification<CatalogItem>
    {
        /*
        /// <summary>
        /// Build Catalog Filter Expression
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="typeId"></param>
        /// <param name="term"></param>
        /// <returns></returns>
        public static Expression<Func<CatalogItem, bool>> BuildCatalogFilterExpression(
            int? brandId, int? typeId, string term
        ){
           return i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
                (!typeId.HasValue || i.CatalogTypeId == typeId) &&
                (string.IsNullOrEmpty(term) || i.Name.Contains(term, System.StringComparison.InvariantCultureIgnoreCase));
        }
        public CatalogFilterSpecification(int? brandId, int? typeId, string term)
            : base(BuildCatalogFilterExpression(brandId, typeId, term))
        {
        }

        OR
        */
        public CatalogFilterSpecification(int? brandId, int? typeId, string term)
            : base(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
                (!typeId.HasValue || i.CatalogTypeId == typeId) &&
                (string.IsNullOrEmpty(term) || i.Name.Contains(term))) // no caso de usar M. SQL SERVER, etc
                // (string.IsNullOrEmpty(term) || i.Name.Contains(term, System.StringComparison.InvariantCultureIgnoreCase)); // em memoria
        {
        }
    }
}
