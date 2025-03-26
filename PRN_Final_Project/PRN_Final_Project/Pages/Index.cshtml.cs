using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Services.Interface;
using Repositories.Entities;
using Microsoft.AspNetCore.Identity;

namespace PRN_Assignment.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string email { get; set; }

        [BindProperty]
        public string password { get; set; }


        private readonly IAccountService _accountService;

        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult OnGet()
        {
            // Check if user is already authenticated
            if (User.Identity.IsAuthenticated)
            {
                var uRole = User.FindFirstValue(ClaimTypes.Role);
                switch (uRole.ToLower())
                {
                    case "user":
                        return RedirectToPage("/Homepage");
                    case "staff":
                        return RedirectToPage("/Staff/Index");
                    case "therapist":
                        return RedirectToPage("/TherapistPage/Index");
                    case "admin":
                        return RedirectToPage("/DashboardAndReport/DashboardAndReport");
                    default:
                        return RedirectToPage();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var account = await _accountService.Login(email, password);


                if (account != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, account.accountID.ToString()),
                        new Claim(ClaimTypes.Email, account.email),
                        new Claim(ClaimTypes.Role, account.role)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = true };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    HttpContext.Session.SetString("Email", email);
                    HttpContext.Session.SetString("UserName", account.userName);
                    HttpContext.Session.SetString("UserId", account.accountID.ToString());

                    switch (account.role.ToLower())
                    {
                        case "user":
                            return RedirectToPage("/Homepage");  
                        case "staff":
                            return RedirectToPage("/Staff/Index");
                        case "therapist":
                            return RedirectToPage("/TherapistPage/Index");
                        case "admin":
                            return RedirectToPage("/DashboardAndReport/DashboardAndReport");
                        default:
                            return RedirectToPage();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login failed. Please check your username and password. If do not already have, please register");

                    return Page();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred during login: {e.Message}");
                Console.WriteLine($"Error: {e}");
                return Page();
            }
        }
    }
}
