using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Book.Areas.Identity.Data;
using static Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal.LoginModel;

namespace Book.Areas.Identity.Pages.Account
{
    public class xacnhanModel : PageModel
    {
        public void OnGet()
        {

        }
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginWith2faModel> _logger;

        public xacnhanModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<LoginWith2faModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public async Task<IActionResult> OnPostAsync( string returnUrl = null)
        {

            var value = Request.Form["otp"];

            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            _logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", user.Id);

            var userId = await _userManager.GetUserIdAsync(user);

            var result = await _signInManager.TwoFactorSignInAsync(TokenOptions.DefaultEmailProvider, value, true, true);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, true, null);

                return LocalRedirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Mã OTP Không Đúng.");
                Redirect("/Account/demo");
            }
            return Page();
        }
    }
}
