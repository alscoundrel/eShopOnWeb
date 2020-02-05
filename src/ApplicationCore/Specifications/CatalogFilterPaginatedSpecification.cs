using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using ApplicationCore.UseTypes;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{
    public class CatalogFilterPaginatedSpecification : CatalogFilterSpecification
    {
        public CatalogFilterPaginatedSpecification(int skip, int take, string term, NamesOrderBy? orderBy, Ordination ordination, int? brandId, int? typeId)
            : base(brandId, typeId, term)
        {
            ApplyPaging(skip, take);
            //ApplyOrderBy(i => i.Name);
            // if(!string.IsNullOrEmpty(orderBy)){
            //     Expression<Func<CatalogItem, object>> expr = x => x.GetType().GetProperty(orderBy).GetValue(x, null);
            //     ApplyOrderBy(expr);
            // }
            //var propertyInfo = (new CatalogItem()).GetType().GetProperty(orderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            //ApplyOrderBy(x => propertyInfo.GetValue(x, null));
            //var x  = new CatalogItem();
            //var y = x.GetType().GetProperty("Name").Name;
            switch (orderBy)
            {
                case NamesOrderBy.Name:
                    if(ordination==Ordination.ASC){ ApplyOrderBy(x => x.Name);}else{ ApplyOrderByDescending(x => x.Name);};
                    break;
                case NamesOrderBy.Price:
                    if(ordination==Ordination.ASC){ ApplyOrderBy(x => x.Price);}else{ ApplyOrderByDescending(x => x.Price);};
                    break;
            }
            
        }

    }
}
