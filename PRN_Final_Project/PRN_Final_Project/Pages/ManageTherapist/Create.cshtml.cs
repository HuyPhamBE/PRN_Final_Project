using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Entities;
using Services.Interface;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PRN_Final_Project.Pages.ManageTherapist
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITherapistService _therapistService;

        public CreateModel(IAccountService accountService, ITherapistService therapistService)
        {
            _accountService = accountService;
            _therapistService = therapistService;
        }

        // Account Properties
        [BindProperty]
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3-50 characters")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        // Therapist Properties
        [BindProperty]
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string FullName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select gender")]
        public bool Gender { get; set; } = true; // Default to Male

        [BindProperty]
        [Required(ErrorMessage = "Specialization is required")]
        [StringLength(100, ErrorMessage = "Specialization cannot exceed 100 characters")]
        public string Major { get; set; }

        [BindProperty]
        [Url(ErrorMessage = "Please enter a valid URL")]
        [Display(Name = "Profile Image URL")]
        public string ImageUrl { get; set; } = "https://example.com/default-therapist.jpg";

        [BindProperty]
        [Required(ErrorMessage = "Experience is required")]
        [Range(0, 50, ErrorMessage = "Experience must be between 0-50 years")]
        [Display(Name = "Years of Experience")]
        public int Exp { get; set; } = 1;

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
                // Create new Account
                var account = new Account
                {
                    accountID = Guid.NewGuid(),
                    userName = Username,
                    email = Email,
                    password = Password,
                    role = "Therapist",
                    status = "Active",
                    createdAt = DateTimeOffset.UtcNow
                };

                // Create new Therapist
                var therapist = new Therapist
                {
                    theraID = Guid.NewGuid(),
                    accountID = account.accountID,
                    fullName = FullName,
                    gender = Gender,
                    major = Major,
                    imageUrl = ImageUrl,
                    exp = Exp,
                    status = "Active",
                    createdAt = DateTimeOffset.UtcNow,
                    updatedAt = DateTimeOffset.UtcNow
                };

                await _therapistService.CreateTherapistWithAccountAsync(account, therapist);

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error creating therapist: {ex.Message}");
                return Page();
            }
        }
    }
}