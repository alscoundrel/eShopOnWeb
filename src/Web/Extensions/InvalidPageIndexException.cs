
using System;

namespace Microsoft.eShopWeb.Web.Extensions
{
        public class InvalidPageIndexException : Exception
    {
        public InvalidPageIndexException(int pageNumber) : base($"Inválid page number {pageNumber}")
        {
        }

        protected InvalidPageIndexException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public InvalidPageIndexException(string message) : base(message)
        {
        }

        public InvalidPageIndexException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    
}