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
    public class DetailsModel : PageModel
    {
        private readonly Tabi.Data.TabiContext _context;

        public DetailsModel(Tabi.Data.TabiContext context)
        {
            _context = context;
        }

        public Entry Entry { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entry = await _context.Entry.FirstOrDefaultAsync(m => m.ID == id);

            if (Entry == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
