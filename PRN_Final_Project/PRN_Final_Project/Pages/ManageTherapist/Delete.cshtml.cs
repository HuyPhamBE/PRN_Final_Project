using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface; // Import service interface
using Repositories.Entities;

namespace PRN_Final_Project.Pages.ManageTherapist
{
    public class DeleteModel : PageModel
    {
        private readonly ITherapistService _therapistService;

        public DeleteModel(ITherapistService therapistService)
        {
            _therapistService = therapistService;
        }

        [BindProperty]
        public Therapist Therapist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapist = await _therapistService.GetTherapistById(id.Value);

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
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                bool success = await _therapistService.DeleteTherapistAsync(id.Value);
                if (!success)
                {
                    return NotFound();
                }
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
