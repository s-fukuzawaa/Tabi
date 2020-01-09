using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabi.Models;
using Tabi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tabi.Pages.Portfolio
{
    //https://docs.microsoft.com/aspnet/core
    public class IntroModel : PageModel
    {
        public PortfolioViewModel ViewModel { get; set; } = new PortfolioViewModel();

        public void OnGet()
        {
            string temp = Request.QueryString.Value;

            string s = temp.Substring(4);
            ViewModel.query = s;
            s = s.Substring(0, s.IndexOf("!"));

            if (s.IndexOf("%20") != -1)
            {
                string city = "";
                string[] split = s.Split("%20");

                for (int n = 0; n < split.Length - 1; n++)
                {
                    city = city + split[n] + " ";
                }
                city = city + split[split.Length - 1];


                ViewModel.city = city;

            }

            else if (s.IndexOf("%20") == -1)
            {
                ViewModel.city = s;
            }
            ViewModel.map = $"https://www.google.com/maps/embed/v1/place?q={s}&key=AIzaSyD9pmMuEb9ym41g6x_iyQV7f3hcZAOZlek";

        }


    }
}