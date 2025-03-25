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

namespace PRN_Final_Project.Pages.ManageTherapist
{
    public class EditModel : PageModel
    {
        private readonly Repositories.DB.ApplicationDbContext _context;

        public EditModel(Repositories.DB.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Therapist Therapist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapist =  await _context.Therapists.FirstOrDefaultAsync(m => m.theraID == id);
            if (therapist == null)
            {
                return NotFound();
            }
            Therapist = therapist;
           ViewData["accountID"] = new SelectList(_context.Accounts, "accountID", "email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Therapist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TherapistExists(Therapist.theraID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TherapistExists(Guid id)
        {
            return _context.Therapists.Any(e => e.theraID == id);
        }
    }
}
