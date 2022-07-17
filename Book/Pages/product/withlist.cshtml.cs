using Book.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Book.Pages.product
{
    public class withlistModel : PageModel
    {
        private readonly Book.Areas.Identity.Data.BookContext _context;
        int _id;
        public withlistModel(Book.Areas.Identity.Data.BookContext context)
        {
            _context = context;
        }
        public IList<withlist> withlists { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.Withlists.FirstOrDefault(x => x.IdentityUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)) != null)
            {
                withlists = await _context.Withlists.Where(x => x.IdentityUserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                    .Include(_ => _.Book)
                    .Include(_ => _.Book.Tacgia)
                   .Include(_ => _.Book.The_Loai).ToListAsync();
            }
        }
    }
}
