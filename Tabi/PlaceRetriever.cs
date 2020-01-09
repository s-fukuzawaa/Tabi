using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using static Tabi.Models.Place;


namespace Tabi
{
    public class PlaceRetriever
    {
        public async Task<PlaceRootObject> GetPlace(string s)
        {
            HttpClient httpClient = new HttpClient();
            string apiUrl = $"https://maps.googleapis.com/maps/api/place/textsearch/json?query={s}&key=AIzaSyD9pmMuEb9ym41g6x_iyQV7f3hcZAOZlek";
            string responseString = await httpClient.GetStringAsync(apiUrl);
            PlaceRootObject geocodes = JsonConvert.DeserializeObject<PlaceRootObject>(responseString);
            return geocodes;
        }
    }
}
