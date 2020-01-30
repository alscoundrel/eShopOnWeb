
using System;

namespace Microsoft.eShopWeb.Web.Services
{
    public class ServicesModelException: Exception {

        public ServicesModelException(string message, Exception innerException = null)
            : base(message, innerException) {

            }
    }
}