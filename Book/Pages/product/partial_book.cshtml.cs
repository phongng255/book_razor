using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Book.Areas.Identity.Data;
using Book.Model;
using System.Security.Claims;

namespace Book.Pages.product
{
    public class partial_bookModel : PageModel
    {
        private readonly Book.Areas.Identity.Data.BookContext _context;

        public partial_bookModel(Book.Areas.Identity.Data.BookContext context)
        {
            _context = context;
        }

        public IList<_Book> _Book { get; set; } = default!;

        public IList<withlist> withlists { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                _Book = await _context.Books
                .Include(_ => _.Tacgia)
                .Include(_ => _.The_Loai).ToListAsync();
                withlists = await _context.Withlists.Where(x => x.IdentityUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();
            }
        }

    }
}
