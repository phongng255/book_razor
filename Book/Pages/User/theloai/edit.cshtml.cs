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

namespace Book.Pages.User.theloai
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
        public The_Loais The_Loais { get; set; } = default!;

        // Get Thể loại Để Sửa
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Check Id Và list Thể Loại có null không
            if (id == null || _context.The_Loais == null)
            {
                
                return NotFound();
            }
            // Tìm Thể Loại Cần update
            var the_loais =  await _context.The_Loais.FirstOrDefaultAsync(m => m.id == id);
            // nếu thể loại không có
            if (the_loais == null)
            {
                return NotFound();
            }
            // Nếu Thể Loại Có Thì Get Thể Thệ
            The_Loais = the_loais;
            return Page();
        }

        // Sửa thể Loại
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Không Đủ Thông Tin !";
                return Page();
            }

            _context.Attach(The_Loais).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!The_LoaisExists(The_Loais.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["Success"] = "Sửa Thành Công !";
            return RedirectToPage("./Index");
        }

        private bool The_LoaisExists(int id)
        {
          return (_context.The_Loais?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
