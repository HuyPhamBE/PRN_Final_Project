using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.DB;
using Repositories.Entities;

namespace PRN_Final_Project.Pages.Staff
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
        ViewData["serviceID"] = new SelectList(_context.Services, "ServiceID", "description");
        ViewData["slotID"] = new SelectList(_context.Slots, "SlotID", "SlotID");
        ViewData["theraID"] = new SelectList(_context.Therapists, "theraID", "fullName");
            return Page();
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
