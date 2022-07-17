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

namespace Book.Pages.User.tacgia
{
    [Authorize(Roles = $"{Constants.Roles.Administrator}")]
    public class listModel : PageModel
    {
        private readonly Book.Areas.Identity.Data.BookContext _context;

        public listModel(Book.Areas.Identity.Data.BookContext context)
        {
            _context = context;
        }

        public IList<Tacgia> Tacgia { get;set; } = default!;

        // Get List Tác Giả
        public async Task OnGetAsync()
        {
            if (_context.Tacgias != null)
            {
                Tacgia = await _context.Tacgias.ToListAsync();
            }
        }
    }
}
