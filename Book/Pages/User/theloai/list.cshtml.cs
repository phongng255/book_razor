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

namespace Book.Pages.User.theloai
{
    [Authorize(Roles = $"{Constants.Roles.Administrator}")]
    public class listModel : PageModel
    {
        private readonly Book.Areas.Identity.Data.BookContext _context;

        public listModel(Book.Areas.Identity.Data.BookContext context)
        {
            _context = context;
        }

        public IList<The_Loais> The_Loais { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // nếu lis thể loại khác null
            if (_context.The_Loais != null)
            {
                The_Loais = await _context.The_Loais.ToListAsync();
            }
        }
    }
}
