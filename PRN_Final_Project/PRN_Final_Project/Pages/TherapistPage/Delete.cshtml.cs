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
    public class DeleteModel : PageModel
    {
        private readonly ITherapistService _therapistService;

        public DeleteModel(ITherapistService therapistService)
        {
            _therapistService = therapistService;
        }

        public Therapist Therapist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty) 
            {
                return NotFound();
            }

            var therapist = await _therapistService.GetTherapistById(id);

            if (therapist == null)
            {
                return NotFound();
            }
            else
            {
                Therapist = therapist;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            try
            {
                await _therapistService.ToggleTherapistStatus(id.Value);
                TempData["Message"] = "Therapist status updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Error: {ex.Message}";
            }

            return RedirectToPage("./Index");
        }

    }
}
