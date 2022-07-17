using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Book.Areas.Identity.Data;
using Book.Model;
using Microsoft.AspNetCore.Authorization;
using Book.Core;

namespace Book.Pages.User.tacgia
{
    [Authorize(Roles = $"{Constants.Roles.Administrator}")]
    public class editModel : PageModel
    {
        private readonly Book.Areas.Identity.Data.BookContext _context;

        public editModel(Book.Areas.Identity.Data.BookContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tacgia Tacgia { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Tacgias == null)
            {
                return NotFound();
            }

            var tacgia =  await _context.Tacgias.FirstOrDefaultAsync(m => m.id == id);
            if (tacgia == null)
            {
                return NotFound();
            }
            Tacgia = tacgia;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Tacgia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TacgiaExists(Tacgia.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TacgiaExists(int id)
        {
          return (_context.Tacgias?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
