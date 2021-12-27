using Ewallet.Models.DTO;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface ICurrencyConversionService
    {
        Task<ConversionRateDTO> GetMarketPrices();

        Task<dynamic> ConvertCurrency(CurrencyConverterDTO currency);
    }
}
