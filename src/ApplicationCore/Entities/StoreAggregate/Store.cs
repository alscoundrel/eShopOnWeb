using System;
using ApplicationCore.UseTypes;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate
{
    public class Store : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
    }
}
