using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;

namespace PRN_Final_Project.Pages.TherapyResultPage
{
    public class DeleteModel : PageModel
    {
        private readonly Repositories.DB.ApplicationDbContext _context;

        public DeleteModel(Repositories.DB.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TherapyResult TherapyResult { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapyresult = await _context.TherapyResults.FirstOrDefaultAsync(m => m.theraResultID == id);

            if (therapyresult == null)
            {
                return NotFound();
            }
            else
            {
                TherapyResult = therapyresult;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapyresult = await _context.TherapyResults.FindAsync(id);
            if (therapyresult != null)
            {
                TherapyResult = therapyresult;
                _context.TherapyResults.Remove(TherapyResult);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
