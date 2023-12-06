using System.Threading.Tasks;
using AddressAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AddressAPI.Pages.Shared
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly UserService _userService;

        [BindProperty]
        public LoginInputModel Input { get; set; }

        public LoginModel(ILogger<LoginModel> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           var existingUser = await _userService.GetUserByEmail(Input.Username);
    if (existingUser == null)
    {
        ModelState.AddModelError(string.Empty, "User not found.");
        return Page();
    }

    // Verify the password
    var isPasswordValid = _userService.VerifyPassword(existingUser, Input.Password);
    if (isPasswordValid)
    {
        return RedirectToPage("/Index");
    }
    else
    {
        ModelState.AddModelError(string.Empty, "Wrong password.");
        return Page();
    }
        }
    }
}
