
using System;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class ModelNotFoundException: Exception {

        public ModelNotFoundException(string message, Exception innerException = null)
            : base(message, innerException) {

            }
    }
}