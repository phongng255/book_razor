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

namespace Book.Pages.User.theloai
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
        public The_Loais The_Loais { get; set; } = default!;


        // Thêm Thể Loại
        public async Task<JsonResult> OnPostAsync(string name)
        {
            try
            {
                // Check tên Thể Loại trong database
                if(_context.The_Loais.Where(x=>x.ten_the_loai == name).FirstOrDefault() != null)
                {
                    // Nếu Trùng Tên
                    return new JsonResult(new { status = 0 });
                }

                // Nếu không null, Lưu Thể Loại vào DataBase
                The_Loais.ten_the_loai = name;
                _context.The_Loais.Add(The_Loais);
                await _context.SaveChangesAsync();

                // Thêm Thành Công
                return new JsonResult(new { status = 1 });
            }
            catch (Exception)
            {
                // Lỗi Ngoại Lệ
                return new JsonResult(new { status = -1 });
            }
        }
        // Xóa Thể Loại
        public async Task<JsonResult> OnPostAsyncDelete(int id)
        {
            try
            {
                // Tìm Thể Loại theo id
                var find = _context.The_Loais.Find(id);
                if (find == null)
                {
                    // nếu thể loại không có .
                    return new JsonResult(new { status = 0 });
                }
                // Nếu Thể Loại Có Thì Xóa Thể Loại
                _context.The_Loais.Remove(find);
                await _context.SaveChangesAsync();

                // Trả Về giá trị Thành Công!
                return new JsonResult(new { status = 1 });
            }
            catch (Exception)
            {
                // Lỗi Ngoại Lệ
                return new JsonResult(new { status = -1 });
            }
        }
    }
}
