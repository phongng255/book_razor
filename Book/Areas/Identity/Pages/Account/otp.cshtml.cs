using Book.Areas.Identity.Data;
using Book.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Book.Areas.Identity.Pages.Account.LoginModel;

namespace Book.Areas.Identity.Pages.Account
{
    public class otpModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginWith2faModel> _logger;
        private readonly ISmsSender _ISmsSender;
        public otpModel(
           SignInManager<ApplicationUser> signInManager,
           UserManager<ApplicationUser> userManager,
           ILogger<LoginWith2faModel> logger, IEmailSender emailSender, ISmsSender _iSmsSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _ISmsSender = _iSmsSender;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            var value = Request.Form["cars"];
            returnUrl ??= Url.Content("~/");
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (value == "app")
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
            }
            if (value == "gmail")
            {
                var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
                await _emailSender.SendEmailAsync(user.Email, "Mã OTP Đăng Nhập",
                        $"Đây Là Mã OTP Đăng Nhập Của bạn <a>{code}</a>.");
                return RedirectToPage("./xacnhan", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
            }
            if (value == "sms")
            {
                if (user.PhoneNumber == null)
                {
                    ModelState.AddModelError(string.Empty, "Mã OTP Không Đúng.");
                    return Page();
                }
                else
                {
                    var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
                    await _ISmsSender.SendSmsAsync(user.PhoneNumber, "Mã OTP Đăng Nhập :" + code);
                }
                return RedirectToPage("./xacnhan", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
            }
            return Page();
        }
    }
}
