using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;
using Services.Interface;

namespace PRN_Final_Project.Pages.TherapistPage
{
    public class EditModel : PageModel
    {
        private readonly ITherapistService _therapistService;
        private readonly IAccountService _accountService;

        public EditModel(ITherapistService therapistService, IAccountService accountService)
        {
            _therapistService = therapistService;
            _accountService = accountService;
        }

        [BindProperty]
        public Therapist Therapist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var therapist = await _therapistService.GetTherapistById(id);
            if (therapist == null)
            {
                return NotFound();
            }

            Therapist = therapist;

            var accounts = await _accountService.GetAllAccount();
            ViewData["accountID"] = new SelectList(accounts, "accountID", "email", Therapist.accountID);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _therapistService.UpdateTherapist(Therapist);
                TempData["Message"] = "Update therapist successfully";
                return RedirectToPage("./Edit", new { id = Therapist.theraID });
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return Page();
            }
        }
    }
}
