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
using Services.Services;

namespace PRN_Final_Project.Pages.TherapyResultPage
{
    public class EditModel : PageModel
    {
        private readonly ITherapistResultService _therapistResultService;
        private readonly IBookingService _bookingService;

        public EditModel(ITherapistResultService therapistResultService, IBookingService bookingService)
        {
            _therapistResultService = therapistResultService;
            _bookingService = bookingService;
        }

        [BindProperty]
        public TherapyResult TherapyResult { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var therapyResult = await _therapistResultService.GetTherapyResultById(id);
            if (therapyResult == null)
            {
                return NotFound();
            }

            TherapyResult = therapyResult;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _therapistResultService.UpdateTherapyResult(TherapyResult);
                TempData["Message"] = "Update therapy result successfully";
                return RedirectToPage("./Edit", new { id = TherapyResult.theraResultID });
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return Page();
            }
        }


    }
}
