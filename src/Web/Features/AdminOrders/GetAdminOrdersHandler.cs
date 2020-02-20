using MediatR;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Features.AdminOrders
{
    public class GetAdminOrdersHandler : IRequestHandler<GetAdminOrders, IEnumerable<OrderViewModel>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAdminOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderViewModel>> Handle(GetAdminOrders request, CancellationToken cancellationToken)
        {
            var specification = new CustomerOrdersWithItemsSpecification();
            var orders = await _orderRepository.ListAsync(specification);
            return orders.Select(o => new OrderViewModel
            {
                OrderDate = o.OrderDate,
                OrderBy = o.BuyerId,
                OrderItems = o.OrderItems?.Select(oi => new OrderItemViewModel()
                {
                    PictureUrl = oi.ItemOrdered.PictureUri,
                    ProductId = oi.ItemOrdered.CatalogItemId,
                    ProductName = oi.ItemOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList(),
                OrderNumber = o.Id,
                ShippingAddress = o.ShipToAddress,
                Status = o.OrderStatus,
                Commentes = o.Comments,
                Total = o.Total()
            });
        }
    }
}
