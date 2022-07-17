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
using Book.Core;
using Microsoft.AspNetCore.Authorization;

namespace Book.Pages.User.book
{
    [Authorize(Roles = $"{Constants.Roles.Administrator}")]
    public class add_bookModel : PageModel
    {
        private readonly Book.Areas.Identity.Data.BookContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public add_bookModel(Book.Areas.Identity.Data.BookContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["tacgiaId"] = new SelectList(_context.Tacgias, "id", "ten_tg");
            ViewData["the_loaiId"] = new SelectList(_context.The_Loais, "id", "ten_the_loai");
            return Page();
        }

        [BindProperty]
        [Required(ErrorMessage = "Chọn một file")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png,jpg,jpeg,gif")]
        [Display(Name = "Chọn file upload")]
        public IFormFile File_PDF { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Chọn một file")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png,jpg,jpeg,gif")]
        [Display(Name = "Chọn file upload")]
        public IFormFile File_img { get; set; }

        [BindProperty]
        public _Book _Book { get; set; } = default!;

        public string ContentRootPath { get; set; }

       
        public async Task<IActionResult> OnPostAsync()
        {
            TempData["Error"] = null;
            TempData["Success"] = null;
            // Kiểm tra null
            if ( _context.Books == null || _Book.TenSach == null || File_PDF == null || File_img == null)
            {
                TempData["Error"] = "Không Đủ Thông Tin !";
                return Redirect("/User/book/add_book");
            }
            else
            {
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
                    _Book.img = uniqueFileName;
                }
                if (File_PDF != null)
                {
                    var uniqueFileName = string.Format(@"{0}.pdf", Guid.NewGuid());
                    var file = Path.Combine(_environment.ContentRootPath, @"wwwroot\uploads", uniqueFileName);
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        File_PDF.CopyTo(fileStream);
                    }
                    _Book.file_name = uniqueFileName;
                }
                _context.Books.Add(_Book);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Thêm Thành Công !";
                return Redirect("/User/book/add_book");
            }
        }
    }
}
