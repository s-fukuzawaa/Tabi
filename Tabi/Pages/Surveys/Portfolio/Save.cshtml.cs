using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tabi.Models;
using Tabi.ViewModels;

namespace Tabi
{
    public class SaveModel : PageModel
    {
        private readonly Tabi.Data.TabiContext _context;
        public string query { get; set; }
        public int delete { get; set; }
        public IList<Entry> show { get; set; }
        public int start { get; set; }
        public SaveModel(Tabi.Data.TabiContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            string s = Request.QueryString.Value.Substring(4);
            query = s;
            string[] t = s.Split("!");
            string city = "";
            IQueryable<Entry> entryIQ = from n in _context.Entry select n;


            if (s.IndexOf("%20") != -1)
            {
                string[] split = s.Split("%20");

                for (int n = 0; n < split.Length - 1; n++)
                {
                    city = city + split[n] + " ";
                }
                city = city + split[split.Length - 1].Substring(0, split[split.Length - 1].IndexOf("!"));


                
            }

            else if (s.IndexOf("%20") == -1)
            {
                city = s.Substring(0, s.IndexOf("!"));
            }
            if(!String.IsNullOrEmpty(city))
            {
                entryIQ = entryIQ.Where(n => n.city.Contains(city));    
            }
            show = await entryIQ.AsNoTracking().ToListAsync();
            
            
        }
        
    }
}