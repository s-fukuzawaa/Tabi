using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tabi.ViewModels;
using Tabi.Models;
using Microsoft.EntityFrameworkCore;
using Tabi.Pages.Portfolio;

namespace Tabi.Pages.Surveys
{
    public class MapModel : PageModel
    {
        private readonly Tabi.Data.TabiContext _context;
        private List<string> temp = new List<string>();
        public MainPageViewModel ViewModel { get; set; } = new MainPageViewModel();
        public MapModel(Tabi.Data.TabiContext context)
        {
            _context = context;
        }
        public Survey Survey { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Survey = await _context.Survey.FirstOrDefaultAsync(m => m.ID == id);
            ViewModel.input = Survey.region;
            GeocodeRetriever geocodeRetriever = new GeocodeRetriever();
            GeocodeRootObject geocodeRootObject = await geocodeRetriever.GetGeocode(ViewModel.input);
            ViewModel.level = geocodeRootObject.results[0].address_components[0].types[0];


            if (ViewModel.level.ToLower().Equals("administrative_area_level_3") || ViewModel.level.ToLower().Equals("locality"))
            {
                ViewModel.capID = geocodeRootObject.results[0].address_components[0].long_name + "!" + id + "!" + _context.Entry.Count();
                return Redirect("https://localhost:44319/surveys/portfolio/intro?id=" + ViewModel.capID);
            }
            await UpdateGeocode(Survey.region.ToLower(), (int)id);
            if (Survey == null)
            {
                return NotFound();
            }
            return Page();

        }

        private async Task UpdateGeocode(string s, int id)
        {
            GeocodeRetriever geocodeRetriever = new GeocodeRetriever();
            if (s.Equals("korea"))
            {
                s = "South Korea";
            }

            GeocodeRootObject geocodeRootObject = await geocodeRetriever.GetGeocode(s);


            ViewModel.level = geocodeRootObject.results[0].address_components[0].types[0];

            //if s is a country, input capitol
           
                if (ViewModel.level.ToLower().Equals("country"))
                {
                    geocodeRootObject = await geocodeRetriever.GetGeocode("capitol+of+" + s);
                    ViewModel.capitol = geocodeRootObject.results[0].address_components[0].long_name; ;
                    ViewModel.capID = geocodeRootObject.results[0].address_components[0].long_name + "!" + id + "!" + _context.Entry.Count();
                }
                geocodeRootObject = await geocodeRetriever.GetGeocode("list+of+popular+cities+in+" + s);
                ViewModel.populars = new List<string>();
                ViewModel.ID = new List<string>();
                //list of popular cities in s
                for (int i = 0; i < geocodeRootObject.results.Count; i++)
                {
                    ViewModel.populars.Add(geocodeRootObject.results[i].formatted_address);
                    ViewModel.ID.Add(geocodeRootObject.results[i].formatted_address + "!" + id + "!" + _context.Entry.Count());
                }

                //map
                ViewModel.center = $"https://www.google.com/maps/embed/v1/place?q={s}&key=AIzaSyD9pmMuEb9ym41g6x_iyQV7f3hcZAOZlek";

            

        }

    }
}