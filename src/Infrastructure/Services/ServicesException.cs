
using System;

namespace Infrastructure.Services
{
    public class ServicesException: Exception {

        public ServicesException(string message, Exception innerException = null)
            : base(message, innerException) {

            }
    }
}