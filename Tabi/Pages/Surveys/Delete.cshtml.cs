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
    public class DeleteModel1 : PageModel
    {
        private readonly Tabi.Data.TabiContext _context;

        public DeleteModel1(Tabi.Data.TabiContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Survey = await _context.Survey.FindAsync(id);

            if (Survey != null)
            {
                _context.Survey.Remove(Survey);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
