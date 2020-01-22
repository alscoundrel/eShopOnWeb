using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{

    /// <summary>
    /// Abstraction for converting monetary values
    /// </summary>
    public interface ICurrencyService
    {
        public enum Currency{
            USD,
            EUR,
            GBP
        }
        
        /// <summary>
        /// Convert currency source to curency target
        /// </summary>
        /// <param name="value"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<decimal> Convert(decimal value, Currency source, Currency target, CancellationToken cancellationToken = default(CancellationToken));
    }
}
