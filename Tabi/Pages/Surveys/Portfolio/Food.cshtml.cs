using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabi.Models;
using Tabi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Tabi.Models.Place;
using System.Collections;
using System.Text;

namespace Tabi.Pages.Portfolio
{
    public class FoodModel : PageModel
    {
        private readonly Tabi.Data.TabiContext _context;
        public PortfolioViewModel ViewModel { get; set; } = new PortfolioViewModel();
        private Dictionary<string, string[]> hash = new Dictionary<string,string[]>();
        private Dictionary<string, double> hash1= new Dictionary<string, double>();


        public CurrencyRootObject CiewModel { get; set; } = new CurrencyRootObject();
        
        public FoodModel(Tabi.Data.TabiContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            string s = Request.QueryString.Value.Substring(4);
            ViewModel.query = s;
            string[] t = s.Split("!");
            ViewModel.ID =t[1];
            if (s.IndexOf("%20") != -1)
            {
                string city = "";
                string[] split = s.Split("%20");

                for (int n = 0; n < split.Length - 1; n++)
                {
                    city = city + split[n] + " ";
                }
                city = city + split[split.Length - 1].Substring(0, split[split.Length - 1].IndexOf("!"));


                ViewModel.city = city;

            } 

            else if (s.IndexOf("%20") == -1)
            {
                ViewModel.city = s.Substring(0, s.IndexOf("!"));
            }
            await UpdatePlace(ViewModel.city);
            


            return Page();
        }

        private async Task UpdatePlace(string s)
        {
            PlaceRetriever placeRetriever = new PlaceRetriever();
            GeocodeRetriever geoRetriever = new GeocodeRetriever();
           
            PlaceRootObject placeRootObject = await placeRetriever.GetPlace("popular+food+places+in+" + s);
            //CurrencyRetriever currencyRetriever = new CurrencyRetriever();
            //CurrencyRootObject currency = await currencyRetriever.GetCurrency();


            ViewModel.placeName = new List<string>();

            ViewModel.imgUrl = new List<string>();
            ViewModel.rating = new List<string>();
            ViewModel.price = new List<string>();
            ViewModel.origin = new List<string>();
            ViewModel.destin = new List<string>();


            CurrencyRetriever c = new CurrencyRetriever();
            CiewModel = await c.GetCurrency();
            

            var csv = new List<CountryCurrencyRootObject>();
            var lines = System.IO.File.ReadAllLines(@"C:\Users\airei\source\repos\Tabi\Tabi\Imgs\cc.txt", Encoding.UTF8);
            foreach (string line in lines)
            {
                string[] tere = line.Split(',');
                //Code = t[3];
                //CountryCode = t[1];
                //Currency = t[2];
                string[] arr = new string[2];
                arr[0] = tere[3];
                arr[1] = tere[2];
                hash.Add(tere[1], arr);

            }


            hash1.Add("CAD", CiewModel.rates.CAD);
            hash1.Add("HKD", CiewModel.rates.HKD);
            hash1.Add("ISK", CiewModel.rates.ISK);
            hash1.Add("PHP", CiewModel.rates.PHP);
            hash1.Add("DKK", CiewModel.rates.DKK);
            hash1.Add("HUF", CiewModel.rates.HUF);
            hash1.Add("CZK", CiewModel.rates.CZK);
            hash1.Add("GBP", CiewModel.rates.GBP);
            hash1.Add("RON", CiewModel.rates.RON);
            hash1.Add("SEK", CiewModel.rates.SEK);
            hash1.Add("IDR", CiewModel.rates.IDR);
            hash1.Add("INR", CiewModel.rates.INR);
            hash1.Add("BRL", CiewModel.rates.BRL);
            hash1.Add("RUB", CiewModel.rates.RUB);
            hash1.Add("HRK", CiewModel.rates.HRK);
            hash1.Add("JPY", CiewModel.rates.JPY);
            hash1.Add("THB", CiewModel.rates.THB);
            hash1.Add("CHF", CiewModel.rates.CHF);
            hash1.Add("EUR", CiewModel.rates.EUR);
            hash1.Add("MYR", CiewModel.rates.MYR);
            hash1.Add("BGN", CiewModel.rates.BGN);
            hash1.Add("TRY", CiewModel.rates.TRY);
            hash1.Add("CNY", CiewModel.rates.CNY);
            hash1.Add("NOK", CiewModel.rates.NOK);
            hash1.Add("NZD", CiewModel.rates.NZD);
            hash1.Add("ZAR", CiewModel.rates.ZAR);
            hash1.Add("USD", CiewModel.rates.USD);
            hash1.Add("MXN", CiewModel.rates.MXN);
            hash1.Add("SGD", CiewModel.rates.SGD);
            hash1.Add("AUD", CiewModel.rates.AUD);
            hash1.Add("ILS", CiewModel.rates.ILS);
            hash1.Add("KRW", CiewModel.rates.KRW);
            hash1.Add("PLN", CiewModel.rates.PLN);

            //resulting food places
            for (int i = 0; i < placeRootObject.results.Count; i++)
            {
                ViewModel.placeName.Add(placeRootObject.results[i].name);
                if (placeRootObject.results[i].photos != null)
                {
                    ViewModel.imgUrl.Add("https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photoreference=" + placeRootObject.results[i].photos[0].photo_reference + "&key=AIzaSyD9pmMuEb9ym41g6x_iyQV7f3hcZAOZlek");
                }
                else if (placeRootObject.results[i].photos == null)
                {
                    ViewModel.imgUrl.Add("0");
                }
                if (placeRootObject.results[i].formatted_address.Contains("#"))
                {
                    string[] help = placeRootObject.results[i].formatted_address.Split("#");
                    string temp = help[0];
                    for(int h=1; h<help.Length;h++)
                    {
                        temp = temp + help[h];
                    }
                    GeocodeRootObject geocodeRootObject = await geoRetriever.GetGeocode("nearest+hotel+from+" +temp);
                    ViewModel.origin.Add(geocodeRootObject.results[0].place_id);
                    ViewModel.destin.Add(placeRootObject.results[i].place_id);
                   

                    ViewModel.price.Add(Math.Round(placeRootObject.results[i].price_level* rating(geocodeRootObject, i),2) + "");
                    ViewModel.rating.Add(placeRootObject.results[i].rating + "");
                }
                else
                {
                    GeocodeRootObject geocodeRootObject = await geoRetriever.GetGeocode("nearest+hotel+from+" + placeRootObject.results[i].formatted_address);
                    ViewModel.origin.Add(geocodeRootObject.results[0].place_id);
                    ViewModel.destin.Add(placeRootObject.results[i].place_id);
                    ViewModel.price.Add(Math.Round(placeRootObject.results[i].price_level*rating(geocodeRootObject, i),2) + "");
                    ViewModel.rating.Add(placeRootObject.results[i].rating + "");
                }

            }
            /* <img src="@Html.ViewData.Model.ViewModel.imgUrl[i]">*/

        }
        [BindProperty]
        public Entry foodEntry { get; set; }
        
       public  double rating(GeocodeRootObject code, int n)
        {

            
            string countrycode = "";
            for(int i=0; i<code.results[0].address_components.Count(); i++)
            {
                if(code.results[0].address_components[i].types[0].Equals("country"))
                {
                    countrycode= code.results[0].address_components[i].short_name;
                    break;
                }
            }
            if(hash.ContainsKey(countrycode))
            {
                if(hash1.ContainsKey(hash[countrycode][0]))
                {
                    return hash1[hash[countrycode][0]];
                }
            }
            return 1;
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Boolean f = true;

            foreach (Entry e in _context.Entry)
            {
                string help = e.address;
                if (e.address.Equals(foodEntry.address))
                {
                    f = false;
                    return Redirect("https://localhost:44319/surveys/portfolio/food?id=" + Request.QueryString.Value.Substring(4));

                }
            }

            if (f)
            {
                _context.Entry.Add(foodEntry);
                await _context.SaveChangesAsync();

            }
            return Redirect("https://localhost:44319/surveys/portfolio/food?id=" + Request.QueryString.Value.Substring(4));
        }


    }
}