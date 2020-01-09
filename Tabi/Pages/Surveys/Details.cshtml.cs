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
    public class DetailsModel1 : PageModel
    {
        private readonly Tabi.Data.TabiContext _context;

        public DetailsModel1(Tabi.Data.TabiContext context)
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

            if (Survey == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
