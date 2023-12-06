using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    public void OnGet()
    {
    }
}

public class LoginModel : PageModel
{
    [BindProperty]
    public LoginInputModel Input { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {

        return RedirectToPage("/Index");
    }
}

public class RegisterModel : PageModel
{
    [BindProperty]
    public RegisterInputModel Input { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {

        return RedirectToPage("/Index");
    }
}

public class LogoutModel : PageModel
{
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {

        return RedirectToPage("/Index");
    }
}

public class LoginInputModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class RegisterInputModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
