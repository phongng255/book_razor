using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Book.Areas.Identity.Data;
using Book.Model;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Book.Core;

namespace Book.Pages.User.book
{
    [Authorize(Roles = $"{Constants.Roles.Administrator}")]
    public class editModel : PageModel
    {
        private readonly Book.Areas.Identity.Data.BookContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public editModel(Book.Areas.Identity.Data.BookContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }


        [BindProperty]
        public _Book _Book { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Chọn một file")]
        [DataType(DataType.Upload)]
        [Display(Name = "Chọn file upload")]
        public IFormFile File_PDF { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Chọn một file")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png,jpg,jpeg,gif")]
        [Display(Name = "Chọn file upload")]
        public IFormFile File_img { get; set; }

        // Get list combobox Tác Giả , Thể Loại.
        public IActionResult OnGet(int id)
        {
            ViewData["tacgiaId"] = new SelectList(_context.Tacgias, "id", "ten_tg");
            ViewData["the_loaiId"] = new SelectList(_context.The_Loais, "id", "ten_the_loai");
            // Tìm Sách Cần Sửa
            _Book = _context.Books.Find(id);
            return Page();
        }
        // Sửa Sách
        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Tìm Theo ID
            var update_Book = _context.Books.Find(id);
            // Khởi Tạo File IMG , PDF .
            string file_img;
            string file_pdf;
            // Nếu Giá trị Cần sửa null
            if (_context.Books == null || _Book == null || update_Book == null)
            {
                return Page();
            }
            // Gán Giá Trị Khởi tạo IMG và PDF
            file_img = update_Book.img;
            file_pdf = update_Book.file_name;
            // Nếu file IMG & PDF Khác Null thì thêm và gán giá trị Cho file PDF và IMG
            if (File_img != null)
            {
                var fileExt = Path.GetExtension(File_img.FileName);
                var uniqueFileName = string.Format(@"{0}{1}", Guid.NewGuid(), fileExt);
                var file = Path.Combine(_environment.ContentRootPath, @"wwwroot\uploads", uniqueFileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    File_img.CopyTo(fileStream);
                }
                file_img = uniqueFileName;
            }
            if (File_PDF != null)
            {
                var uniqueFileName = string.Format(@"{0}.pdf", Guid.NewGuid());
                var file = Path.Combine(_environment.ContentRootPath, @"wwwroot\uploads", uniqueFileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    File_PDF.CopyTo(fileStream);
                }
                file_pdf = uniqueFileName;
            }
            // Cập Nhật Giá trị Vào Book
            update_Book.TenSach = _Book.TenSach;
            update_Book.tacgiaId = _Book.tacgiaId;
            update_Book.the_loaiId = _Book.the_loaiId;
            update_Book.file_name = file_pdf;
            update_Book.img = file_img;
            await _context.SaveChangesAsync();
            // Trả Về Thành Công
            TempData["Success"] = "Sửa Thành Công !";
            return RedirectToPage("/User/book/add_book");
        }
    }
}
