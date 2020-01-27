using ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using static ApplicationCore.Interfaces.ICurrencyService;

namespace Infrastructure.Services
{
    public class CurrencyServiceStatic : ICurrencyService
    {


        public Task<decimal> Convert(decimal value, Currency source, Currency target, CancellationToken cancellationToken = default(CancellationToken))
        {            
            Dictionary<string, decimal> rats = new Dictionary<string, decimal>();
            rats.Add("USD", 1.00m);
            rats.Add("EUR", 1.99m);
            rats.Add("GBP", 1.55m);

            var rat = 1.03m;
            rats.TryGetValue(target.ToString(), out rat);

            //throw new System.NotImplementedException();
            return Task.FromResult(value*rat); // TODO: miss implementation
        }
    }
}
