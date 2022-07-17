using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Book.Areas.Identity.Data;
using Book.Model;
using Microsoft.AspNetCore.Authorization;
using Book.Core;

namespace Book.Pages.User.book
{
    [Authorize(Roles = $"{Constants.Roles.Administrator}")]
    public class listModel : PageModel
    {
        private readonly Book.Areas.Identity.Data.BookContext _context;

        public listModel(Book.Areas.Identity.Data.BookContext context)
        {
            _context = context;
        }

        public IList<_Book> _Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                _Book = await _context.Books
                .Include(_ => _.Tacgia)
                .Include(_ => _.The_Loai).ToListAsync();
            }
        }
        // Xóa sách
        [HttpPost]
        public JsonResult OnPostDelete(int id)
        {
            // Tìm Sách theo id
            var delete = _context.Books.Find(id);
            if(delete == null)
            {
                // Không tìm thấy sáchs
                return new JsonResult(new { status = false });
            }
            // Xóa File PDF Và IMG
            System.IO.File.Delete(@"wwwroot\uploads\"+ delete.file_name);
            System.IO.File.Delete(@"wwwroot\uploads\" + delete.img);
            // Xóa Sách 
            _context.Books.Remove(delete);
            _context.SaveChanges();
            // Trả Về Kết quả Xóa Thành công
            return new JsonResult(new { status = true });
        }
    }
}
