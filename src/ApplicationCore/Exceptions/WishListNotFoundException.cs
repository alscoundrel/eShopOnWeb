using System;

namespace Microsoft.eShopWeb.ApplicationCore.Exceptions
{
    public class WishListNotFoundException : Exception
    {
        public WishListNotFoundException(int basketId) : base($"No wish list found with id {basketId}")
        {
        }

        protected WishListNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public WishListNotFoundException(string message) : base(message)
        {
        }

        public WishListNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
