using ApplicationCore.Interfaces;
<<<<<<< HEAD
using System.Collections.Generic;
=======
>>>>>>> 7593090aa30397e7f73b49dbc228a9cf3acf745c
using System.Threading;
using System.Threading.Tasks;
using static ApplicationCore.Interfaces.ICurrencyService;

namespace Infrastructure.Services
{
    public class CurrencyServiceStatic : ICurrencyService
    {
<<<<<<< HEAD


=======
>>>>>>> 7593090aa30397e7f73b49dbc228a9cf3acf745c
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
