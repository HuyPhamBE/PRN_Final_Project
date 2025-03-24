using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Services.Interface;

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

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var account = await _accountService.Login(email, password);
                    if (account != null)
                {
                    Console.WriteLine("Login successfully");
                    HttpContext.Session.SetString("Email", email);
                     return RedirectToPage("/DashboardAndReport");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login failed. Please check your username and password. If do not already have, please register");
                    Console.WriteLine("Login failed. Please check your username and password. If do not already have, please register");
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
