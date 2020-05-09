﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Tabi.Models;

/*linear-gradient(to bottom right, rgb(102, 255, 255),rgb(51, 204, 255),rgb(0, 204, 255),rgb(102, 255, 255))*/


namespace Tabi
{
    public class GeocodeRetriever
    {
        public async Task<GeocodeRootObject> GetGeocode(string s)
        {
            HttpClient httpClient = new HttpClient();
            string apiUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={s}&key=AIzaSyD9pmMuEb9ym41g6x_iyQV7f3hcZAOZlek";
            string responseString = await httpClient.GetStringAsync(apiUrl);
            GeocodeRootObject geocodes = JsonConvert.DeserializeObject<GeocodeRootObject>(responseString);
            
            return geocodes;

        }
    }
}
