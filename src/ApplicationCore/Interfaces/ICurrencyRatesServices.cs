using System.Threading;
using System.Threading.Tasks;
using static ApplicationCore.Interfaces.ICurrencyService;

namespace ApplicationCore.Interfaces
{

    /// <summary>
    /// Abstraction for request rates list
    /// </summary>
    public interface ICurrencyRatesService
    {
        
        /// <summary>
        /// Convert currency source to curency target
        /// </summary>
        /// <param name="source"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> Request(Currency source, CancellationToken cancellationToken = default(CancellationToken));
    }
}
