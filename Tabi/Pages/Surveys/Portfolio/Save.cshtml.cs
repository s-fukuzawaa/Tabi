using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tabi.Models;
using Tabi.ViewModels;

namespace Tabi
{
    public class SaveModel : PageModel
    {
        private readonly Tabi.Data.TabiContext _context;
        public string query { get; set; }

        public List<Entry> show { get; set; } = new List<Entry>();
        public int start { get; set; }
        public SaveModel(Tabi.Data.TabiContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            string s = Request.QueryString.Value.Substring(4);
            query = s;
            start = int.Parse(s.Split("!")[2]);
            foreach(Entry e in _context.Entry)
            {
                show.Add(e);
            }

            
        }
    }
}