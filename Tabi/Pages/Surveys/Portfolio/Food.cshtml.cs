using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabi.Models;
using Tabi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Tabi.Models.Place;

namespace Tabi.Pages.Portfolio
{
    public class FoodModel : PageModel
    {
        private readonly Tabi.Data.TabiContext _context;
        public PortfolioViewModel ViewModel { get; set; } = new PortfolioViewModel();
        
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

            ViewModel.placeName = new List<string>();

            ViewModel.imgUrl = new List<string>();
            ViewModel.rating = new List<string>();
            ViewModel.price = new List<string>();
            ViewModel.origin = new List<string>();
            ViewModel.destin = new List<string>();

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
                    ViewModel.price.Add(placeRootObject.results[i].price_level + "");
                    ViewModel.rating.Add(placeRootObject.results[i].rating + "");
                }
                else
                {
                    GeocodeRootObject geocodeRootObject = await geoRetriever.GetGeocode("nearest+hotel+from+" + placeRootObject.results[i].formatted_address);
                    ViewModel.origin.Add(geocodeRootObject.results[0].place_id);
                    ViewModel.destin.Add(placeRootObject.results[i].place_id);
                    ViewModel.price.Add(placeRootObject.results[i].price_level + "");
                    ViewModel.rating.Add(placeRootObject.results[i].rating + "");
                }

            }
            /* <img src="@Html.ViewData.Model.ViewModel.imgUrl[i]">*/

        }
        [BindProperty]
        public Entry foodEntry { get; set; }
        
        
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