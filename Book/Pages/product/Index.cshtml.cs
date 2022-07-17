using Book.Areas.Identity.Data;
using Book.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Book.Pages.product
{
    public class IndexModel : PageModel
    {
        private readonly Book.Areas.Identity.Data.BookContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public IndexModel(UserManager<ApplicationUser> userManager, Book.Areas.Identity.Data.BookContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        public IList<_Book> _Book { get; set; }
        public string name { get; set; }
        public IList<withlist> withlists {get;set;}
        public async Task OnGetAsync([FromQuery] int? id)
        {
            name = _context.The_Loais.Find(id).ten_the_loai.ToString();
            if (_context.Books.Where(x=>x.the_loaiId == id) != null)
            {
                _Book = await _context.Books.Where(x => x.the_loaiId == id)
                .Include(_ => _.Tacgia)
                .Include(_ => _.The_Loai).ToListAsync();
                withlists = await _context.Withlists.Where(x => x.IdentityUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();
            }
        }
        public JsonResult OnPostWith_list(int id)
        {
            try
            {
                var find = _context.Withlists.FirstOrDefault(x => x.bookId == id && x.IdentityUserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (find != null)
                {
                    _context.Withlists.Remove(find);
                    _context.SaveChanges();
                    return new JsonResult(new { status = 0 });
                }
                else
                {
                    withlist add = new withlist
                    {
                        bookId = id,
                        IdentityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    };
                    _context.Withlists.Add(add);
                    _context.SaveChanges();
                    return new JsonResult(new { status = 1 });
                }
            }
            catch (Exception)
            {
                return new JsonResult(new { status = 2 });
            }
        }
        public JsonResult OnPostShow_pdf(string fileName)
        {
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, @"uploads\" + fileName);
            if (User.Identity.Name == null)
            {
                return new JsonResult(new { status = false });
            }
            else if (System.IO.File.Exists(filePath))
            {
                return new JsonResult(new {file = filePath.ToString() }) ;
            }
            else
            {
                return new JsonResult(new { status = false });
            }
        }
        public async Task<JsonResult> OnGetAsyncRole()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var userRolesInDb = await _userManager.GetRolesAsync(user);
            foreach (var item in userRolesInDb)
            {
                if (item == "User")
                {
                    return new JsonResult(new { status = true });
                }
            }
            return new JsonResult(new { status = false });
        }
    }
}
