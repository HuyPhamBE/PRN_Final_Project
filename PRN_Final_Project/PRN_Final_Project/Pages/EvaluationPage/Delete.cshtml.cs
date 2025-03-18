using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;

namespace PRN_Final_Project.Pages.EvaluationPage
{
    public class DeleteModel : PageModel
    {
        private readonly Repositories.DB.ApplicationDbContext _context;

        public DeleteModel(Repositories.DB.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Evaluation Evaluation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations.FirstOrDefaultAsync(m => m.evaID == id);

            if (evaluation == null)
            {
                return NotFound();
            }
            else
            {
                Evaluation = evaluation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation != null)
            {
                Evaluation = evaluation;
                _context.Evaluations.Remove(Evaluation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
