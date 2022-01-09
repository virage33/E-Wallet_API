using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ewallet.Core.Interfaces;
using Ewallet.Models.DTO;

namespace Ewallet.Core.Implementations
{
    public class CurrencyConversionService:ICurrencyConversionService
    {
        private readonly string freeApiKey = "bd688d4a8a5364f84700";
        private readonly string fixerApiKey = "2c7cb50f5f691eaf7e1a35c0066a52a4";//"15687e390291e47683c53438e095a709";
        public async Task<dynamic> ConversionRate(CurrencyConverterDTO currency)
        {
          
            string url = $"http://data.fixer.io/api/convert?access_key=2c7cb50f5f691eaf7e1a35c0066a52a4&from={currency.From}&to={currency.To}&amount={currency.amount}";
            string freeapiconverturl = $"https://free.currconv.com/api/v7/convert?q={currency.From}_{currency.To},{currency.To}_{currency.From}&compact=ultra&apiKey={freeApiKey}";
            using var client = new HttpClient();
            var result = await client.GetStringAsync(freeapiconverturl);
            dynamic json = JsonConvert.DeserializeObject<dynamic>(result);

            return json;
        }

        public async Task<dynamic> ConvertCurrency(CurrencyConverterDTO currency)
        {
            ConversionRateDTO response = await GetMarketPrices();
            var toRate = response.Rates[currency.To.ToUpper()];
            var fromRate = response.Rates[currency.From.ToUpper()];
            var convertedToEuro = currency.amount/Convert.ToDecimal(fromRate) ;
            
            
            var result = convertedToEuro * Convert.ToDecimal(toRate);
            return result;
        }

        public async Task<ConversionRateDTO> GetMarketPrices()
        {         
            string url = "http://data.fixer.io/api/latest?access_key=15687e390291e47683c53438e095a709";
            //string url = $"https://free.currconv.com/api/v7/currencies?apiKey={freeApiKey}";
            using var client = new HttpClient();
            var result = await client.GetStringAsync(url);
            ConversionRateDTO json = JsonConvert.DeserializeObject<ConversionRateDTO>(result);
            return json;
        }

       
    }
}
