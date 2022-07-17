using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Book.Areas.Identity.Data;
using Book.Model;
using Microsoft.AspNetCore.Authorization;
using Book.Core;

namespace Book.Pages.User.tacgia
{
    [Authorize(Roles = $"{Constants.Roles.Administrator}")]
    public class indexModel : PageModel
    {
        private readonly Book.Areas.Identity.Data.BookContext _context;

        public indexModel(Book.Areas.Identity.Data.BookContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Tacgia Tacgia { get; set; } = default!;


        // Thêm Tác Giả
        public async Task<JsonResult> OnPostAsync(string name)
        {
            try
            {
                // Tìm Tác Giả
                if (_context.The_Loais.Where(x => x.ten_the_loai == name).FirstOrDefault() != null)
                {
                    // nếu trùng tên
                    return new JsonResult(new { status = 0 });
                }
                // nếu không trùng tên
                Tacgia.ten_tg = name;
                _context.Tacgias.Add(Tacgia);
                await _context.SaveChangesAsync();

                // trả về thành công
                return new JsonResult(new { status = 1 });
            }
            catch (Exception)
            {
                // lỗi ngoại lệ
                return new JsonResult(new { status = -1 });
            }
        }
        // Xóa Tác Giả
        public async Task<JsonResult> OnPostAsyncDelete(int id)
        {
            try
            {
                // Tìm Tác giả
                var find = _context.Tacgias.Find(id);

                // Nếu tác giả không tìm thấy
                if (find == null)
                {
                    // Không tìm thấy tác giả
                    return new JsonResult(new { status = 0 });
                }
                // Xóa Tác Giả
                _context.Tacgias.Remove(find);
                await _context.SaveChangesAsync();
                // Xóa Thành Công
                return new JsonResult(new { status = 1 });
            }
            catch (Exception)
            {
                // Trả về lỗi Ngoại lệ
                return new JsonResult(new { status = -1 });
            }
        }
    }
}
