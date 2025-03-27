using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.DB;
using Repositories.Entities;
using Services.Interface;

namespace PRN_Final_Project.Pages.TherapistPage
{
    public class CreateModel : PageModel
    {
        private readonly ITherapistService _therapistService;
        private readonly IAccountService _accountService; 

        public CreateModel(ITherapistService therapistService)
        {
            _therapistService = therapistService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var accounts = await _accountService.GetAllAccount();
            ViewData["accountID"] = new SelectList(accounts, "accountID", "email");
            return Page();
        }

        [BindProperty]
        public Therapist Therapist { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.Therapists.Add(Therapist);
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}
    }
}
