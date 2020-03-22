using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tabi.Data;
using Tabi.Models;

namespace Tabi
{
    public class IndexModel : PageModel
    {
        private readonly Tabi.Data.TabiContext _context;

        public IndexModel(Tabi.Data.TabiContext context)
        {
            _context = context;
        }
        public string CitySort { get; set; }
        public string typesort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Entry> Entry { get;set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            CitySort = String.IsNullOrEmpty(sortOrder) ? "name_desc_city" : "";
            typesort = String.IsNullOrEmpty(sortOrder) ? "name_desc_placetype" : "";

            CurrentFilter = searchString;
            IQueryable < Entry > entryIQ= from s in _context.Entry select s;

            if(!String.IsNullOrEmpty(searchString))
            {
                entryIQ = entryIQ.Where(s => s.placetype.Contains(searchString) || s.city.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc_city":
                    entryIQ = entryIQ.OrderByDescending(s => s.city);
                    break;

                case "name_desc_placetype":
                    entryIQ = entryIQ.OrderByDescending(s => s.placetype);
                    break;
                default:
                    entryIQ = entryIQ.OrderBy(s => s.city);
                    break;
            }


            Entry = await entryIQ.AsNoTracking().ToListAsync();
        }
    }
}
