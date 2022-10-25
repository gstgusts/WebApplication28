using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cars.Data;
using Cars.Data.Models;

namespace WebApplication28.Pages.Owners
{
    public class DeleteModel : PageModel
    {
        private readonly Cars.Data.ApplicationDbContext _context;

        public DeleteModel(Cars.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Owner Owner { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Owner == null)
            {
                return NotFound();
            }

            var owner = await _context.Owner.FirstOrDefaultAsync(m => m.Id == id);

            if (owner == null)
            {
                return NotFound();
            }
            else 
            {
                Owner = owner;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Owner == null)
            {
                return NotFound();
            }
            var owner = await _context.Owner.FindAsync(id);

            if (owner != null)
            {
                Owner = owner;
                _context.Owner.Remove(Owner);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
