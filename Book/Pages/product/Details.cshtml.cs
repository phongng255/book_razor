using Book.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Book.Pages.product
{
    public class DetailsModel : PageModel
    {
        private readonly Book.Areas.Identity.Data.BookContext _context;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public DetailsModel( Book.Areas.Identity.Data.BookContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public _Book product { get; set; }
        public IList<withlist> withlists { get; set; }

        [HttpGet]
        public async Task OnGetAsync(int id)
        {
            var list_book = await _context.Books
                .Include(_ => _.Tacgia)
                .Include(_ => _.The_Loai).ToListAsync();
            withlists = await _context.Withlists.Where(x => x.IdentityUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();
            product = _context.Books.Find(id);
        }
        [Authorize]
        [HttpPost]
        public ActionResult OnPostDownload(String file)
        {
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, @"uploads\" + file);
            if (User.Identity.Name == null)
            {
                return Redirect("/Identity/Account/Login");
            }
			else
			{
                var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
                var a  = File(fileStream, "application/force-download", file);
                return File(fileStream, "application/force-download", file);
			}
        }
    }
}
