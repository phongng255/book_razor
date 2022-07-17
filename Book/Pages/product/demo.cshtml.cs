using Book.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Book.Pages.product
{
    [Authorize(Roles = $"{Constants.Roles.User}")]
    public class demoModel : PageModel
    {
        public FileResult OnGet(string path)
        {
            var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            return File(fileStream, "application/pdf");
        }
    }
}
