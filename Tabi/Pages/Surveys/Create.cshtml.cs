﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tabi.Data;
using Tabi.Models;

namespace Tabi
{
    public class CreateModel1 : PageModel
    {
        private readonly Tabi.Data.TabiContext _context;

        public CreateModel1(Tabi.Data.TabiContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Survey Survey { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Survey.Add(Survey);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
