using Ewallet.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ewallet.Core.Interfaces
{
    public interface ICurrencyConversionService
    {
        Task<dynamic> GetMarketPrices();

        Task<dynamic> ConvertCurrency(CurrencyConverterDTO currency);
    }
}
