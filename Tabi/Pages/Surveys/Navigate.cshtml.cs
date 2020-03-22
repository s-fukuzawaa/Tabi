using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tabi
{
    public class NavigateModel : PageModel
    {
        private readonly Tabi.Data.TabiContext _context;

        public Boolean preornext { get; set; }
        public int a = 0;
        public List<String> slides { get; set; }
        public void OnGet()
        {
            slides = new List<String>();
            for (int i = 1; i < 6; i++)
            {
                if (i == 2 || i == 5)
                {
                    slides.Add(i + ".PNG");
                }
                else
                {
                    slides.Add(i + ".png");
                }
            }
        }
        public NavigateModel(Tabi.Data.TabiContext context)
        {
            _context = context;
        }
        public void up()
        {
            if (a + 1 == 6)
            {
                a = 0;
            }
            else
            {
                a++;
            }
        }

        


    }
}