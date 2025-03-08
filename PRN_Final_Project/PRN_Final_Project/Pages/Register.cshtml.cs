using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Entities;
using Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace PRN_Final_Project.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountService _accountService;

        public RegisterModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        [Required]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var newAccount = new Account
                {
                    accountID = Guid.NewGuid(),
                    userName = UserName,
                    email = Email,
                    password = Password,
                    role = "User",
                    status = "Active",
                    createdAt = DateTimeOffset.UtcNow
                };

                Console.WriteLine($"Registering Account: {newAccount.userName}, {newAccount.email}, {newAccount.password}");

                var newCustomer = new Customer
                {
                    cusID = Guid.NewGuid(),
                    accountID = newAccount.accountID,
                    fullName = "New User",
                    phone = "",  
                    address = "",
                    dob = DateTime.MinValue, 
                    gender = false,
                    imageUrl = "",
                    createdAt = DateTimeOffset.UtcNow
                };

                await _accountService.register(newAccount, newCustomer);

                return RedirectToPage("/Index");
            } catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

    }
}
