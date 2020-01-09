using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Tabi.Models;
namespace Tabi
{
    public class CurrencyRetriever
    {
        public async Task<CurrencyRootObject> GetCurrency()
        {
            HttpClient httpClient = new HttpClient();
            string apiUrl = "https://api.exchangeratesapi.io/latest?base=USD";
            string response = await httpClient.GetStringAsync(apiUrl);
            CurrencyRootObject currencies = JsonConvert.DeserializeObject<CurrencyRootObject>(response);
            return currencies;
        }
    }
}
