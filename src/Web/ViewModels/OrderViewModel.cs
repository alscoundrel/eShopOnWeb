using ApplicationCore.UseTypes;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class OrderViewModel
    {
        public string OrderBy { get; set; }
        public int OrderNumber { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; }
        public Address ShippingAddress { get; set; }
        public string Commentes { get; set;}
        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
    }
}
