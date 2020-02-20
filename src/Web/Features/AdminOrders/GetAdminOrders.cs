using MediatR;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.Web.Features.AdminOrders
{
    public class GetAdminOrders : IRequest<IEnumerable<OrderViewModel>>
    {
        public GetAdminOrders()
        {
        }
    }
}
