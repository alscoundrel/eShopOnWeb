using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Microsoft.eShopWeb.Web.Middleware
{
    public class BuildCultureRouteModelConvention : IPageRouteModelConvention
    {
        public void Apply(PageRouteModel model)
        {
            List<SelectorModel> selectorModels = new List<SelectorModel>();
            foreach (var selectorModel in model.Selectors.ToList())
            {
                var routeTemplate = selectorModel.AttributeRouteModel.Template;
                selectorModels.Add(new SelectorModel(){
                    AttributeRouteModel = new AttributeRouteModel
                    {
                        Template = "/{culture}/" + routeTemplate
                    }
                });
            }
            foreach(var selectorModel in selectorModels){
                model.Selectors.Add(selectorModel);
            }
        }
    }
}