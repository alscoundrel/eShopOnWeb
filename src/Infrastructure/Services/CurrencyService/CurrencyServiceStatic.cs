using ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using static ApplicationCore.Interfaces.ICurrencyService;

namespace Infrastructure.Services
{
    public class CurrencyServiceStatic : ICurrencyService
    {
        private readonly IConfiguration _configuration;
        public CurrencyServiceStatic(IConfiguration config){
            _configuration = config;
        }

        public Task<decimal> Convert(decimal value, Currency source, Currency target, CancellationToken cancellationToken = default(CancellationToken))
        {   
            var sourceRateString = _configuration.GetValue<string>($"rates:{source}");
            var targetRateString = _configuration.GetValue<string>($"rates:{target}");
            if(string.IsNullOrEmpty(sourceRateString) && string.IsNullOrEmpty(targetRateString)){
                //Para o caso de nao encontrar as taxas passadas no ficheiro json
                Dictionary<string, decimal> rats = new Dictionary<string, decimal>();
                rats.Add("USD", 1.00m);
                rats.Add("EUR", 1.99m);
                rats.Add("GBP", 1.55m);


                var rat = 1.03m;
                rats.TryGetValue(target.ToString(), out rat);

                //throw new System.NotImplementedException();
                return Task.FromResult(value*rat); // TODO: miss implementation
            }
            else{
                var sourceRate = decimal.Parse(sourceRateString, CultureInfo.InvariantCulture);
                var targetRate = decimal.Parse(targetRateString, CultureInfo.InvariantCulture);
                if(targetRate == 0){ throw new ServicesException("Invalid target rate value");}
                
                return Task.FromResult((decimal) value*(sourceRate/targetRate));
            }

        }
    }
}
