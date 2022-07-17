using Book.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Book.Pages.product
{
    public class FindModel : PageModel
    {
        private readonly Book.Areas.Identity.Data.BookContext _context;
        public FindModel(Book.Areas.Identity.Data.BookContext context)
        {
            _context = context;
        }

        public IList<_Book> _Book { get; set; } = default!;

        public IList<withlist> withlists { get; set; }

        // Tìm Kiếm 
        public async Task OnGetAsync(string key)
        {
            if (_context.Books != null)
            {
                _Book = await _context.Books
                .Include(_ => _.Tacgia)
                .Include(_ => _.The_Loai).Where(x => x.TenSach.Contains(key) || string.Compare(x.Tacgia.ten_tg, key) == 0 || string.Compare(x.The_Loai.ten_the_loai, key) == 0).ToListAsync();
                withlists = await _context.Withlists.Where(x => x.IdentityUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();
            }
        }
    }
}
