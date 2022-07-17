using Book.Areas.Identity.Data;
using Book.Core;
using Book.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Book.Pages.User
{
    [Authorize(Roles = $"{Constants.Roles.Administrator}")]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // khởi tạo IndexModel
        public IndexModel(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }

        // Khởi tạo users
        public ICollection<ApplicationUser> users { get; set; }
        
        public void OnGet()
        {
            // Get user
            users = _unitOfWork.User.GetUsers();
        }
        [HttpPost]
        public async Task<JsonResult> OnPostFind(string id)
        {
            // Get user 
            var user = _unitOfWork.User.GetUser(id);
            // get role
            var roles = _unitOfWork.Role.GetRoles();

            // Tìm Role của user
            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            return new JsonResult(new { userRoles = userRoles });
        }
        // update role user
        [HttpPost]
        public async Task<JsonResult> OnPostUpdate(string id, bool User, bool Administrator)
        {
            try
            {
                // Tìm User
                var user = _unitOfWork.User.GetUser(id);
                // get role user
                var roles = _unitOfWork.Role.GetRoles();

                var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
                var rolesToAdd = new List<string>();

                var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

                //.. Xoa Role đã tồn tại.
                foreach (string role in userRolesInDb)
                {
                    await _signInManager.UserManager.RemoveFromRoleAsync(user, role);
                }

                // nếu user khác không có thì add user vào để thêm
                set_roles(rolesToAdd, User, "User");

                // nếu đã tồn tại role đó thì add vào list để xóa
                set_roles(rolesToAdd, Administrator, "Administrator");
                if (rolesToAdd.Any())
                {
                    await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
                }
                

                return new JsonResult(new { status = true });
            }
            catch (Exception)
            {

                return new JsonResult(new { status = false });
            }
        }
        public void set_roles(List<string> rolesToAdd, bool check, string role)
        {
            if (check != false) rolesToAdd.Add(role);
        }


    }
}
