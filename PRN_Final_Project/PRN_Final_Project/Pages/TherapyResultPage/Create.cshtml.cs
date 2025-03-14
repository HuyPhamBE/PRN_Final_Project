using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.DB;
using Repositories.Entities;

namespace PRN_Final_Project.Pages.TherapyResultPage
{
    public class CreateModel : PageModel
    {
        private readonly Repositories.DB.ApplicationDbContext _context;

        public CreateModel(Repositories.DB.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["bookingID"] = new SelectList(_context.Bookings, "BookingID", "status");
            return Page();
        }

        [BindProperty]
        public TherapyResult TherapyResult { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TherapyResults.Add(TherapyResult);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
