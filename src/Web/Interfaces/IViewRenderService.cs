using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Interfaces
{
    public interface IViewRenderService{
        Task<string> RenderToStringAsync(string viewName, object model);
    }
    
}