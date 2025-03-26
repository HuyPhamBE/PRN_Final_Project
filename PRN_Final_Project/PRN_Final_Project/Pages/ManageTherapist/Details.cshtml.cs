using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;

namespace PRN_Final_Project.Pages.ManageTherapist
{
    public class DetailsModel : PageModel
    {
        private readonly Repositories.DB.ApplicationDbContext _context;

        public DetailsModel(Repositories.DB.ApplicationDbContext context)
        {
            _context = context;
        }

        public Therapist Therapist { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var therapist = await _context.Therapists.FirstOrDefaultAsync(m => m.theraID == id);
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
    }
}
