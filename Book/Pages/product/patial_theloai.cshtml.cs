using Book.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Book.Pages.product
{
    public class patial_theloaiModel : PageModel
    {
        private readonly Book.Areas.Identity.Data.BookContext _context;
        public patial_theloaiModel(Book.Areas.Identity.Data.BookContext context)
        {
            _context = context;
        }
        public IList<The_Loais> the_Loais { get; set; } = default!;
        public async Task OnGetAsync()
        {
            if (_context.The_Loais != null)
            {
                the_Loais = await _context.The_Loais.ToListAsync();
            }
        }
    }
}
