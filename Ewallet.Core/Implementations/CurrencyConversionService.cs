using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ewallet.Core.Interfaces;
using Ewallet.Core.DTO;

namespace Ewallet.Core.Implementations
{
    public class CurrencyConversionService:ICurrencyConversionService
    {
        private readonly string freeApiKey = "bd688d4a8a5364f84700";
        private readonly string fixerApiKey = "15687e390291e47683c53438e095a709";
        public async Task<dynamic> ConversionRate(CurrencyConverterDTO currency)
        {
          
            string url = $"http://data.fixer.io/api/convert?access_key=15687e390291e47683c53438e095a709&from={currency.From}&to={currency.To}&amount={currency.amount}";
            string freeapiconverturl = $"https://free.currconv.com/api/v7/convert?q={currency.From}_{currency.To},{currency.To}_{currency.From}&compact=ultra&apiKey={freeApiKey}";
            using var client = new HttpClient();
            var result = await client.GetStringAsync(freeapiconverturl);
            dynamic json = JsonConvert.DeserializeObject<dynamic>(result);

            return json;
        }

        public async Task<dynamic> ConvertCurrency(CurrencyConverterDTO currency)
        {
            var response = await ConversionRate(currency);
            return response;
        }

        public async Task<dynamic> GetMarketPrices()
        {
           
           
            string url = "http://data.fixer.io/api/latest?access_key=15687e390291e47683c53438e095a709";
            //string url = $"https://free.currconv.com/api/v7/currencies?apiKey={freeApiKey}";
            using var client = new HttpClient();
            var result = await client.GetStringAsync(url);
            dynamic json = JsonConvert.DeserializeObject(result);
            return json;
        }

       
    }
}
