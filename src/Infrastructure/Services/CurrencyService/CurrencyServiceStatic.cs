using ApplicationCore.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using static ApplicationCore.Interfaces.ICurrencyService;

namespace Infrastructure.Services
{
    public class CurrencyServiceStatic : ICurrencyService
    {
        public Task<decimal> Convert(decimal value, Currency source, Currency target, CancellationToken cancellationToken = default(CancellationToken))
        {
            //throw new System.NotImplementedException();
            return Task.FromResult(value*1.03m); // TODO: miss implementation
        }
    }
}
