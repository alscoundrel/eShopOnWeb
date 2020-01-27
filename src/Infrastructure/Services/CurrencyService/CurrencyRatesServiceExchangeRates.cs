using ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static ApplicationCore.Interfaces.ICurrencyService;

namespace Infrastructure.Services
{
    public class CurrencyRatesServiceExchangeRates : ICurrencyRatesService
    {
        static HttpClient _client = new HttpClient();

        public async Task<string> Request(Currency source, CancellationToken cancellationToken = default)
        {
            if(string.IsNullOrEmpty(source.ToString())){ throw new System.Exception("Invalid Currency source");}

            var apiPath = $"https://api.exchangeratesapi.io/latest?base=source.ToString()";
            
            HttpResponseMessage response = await _client.GetAsync(apiPath);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return "";//await Task.FromResult("result");
        }
    }
}
