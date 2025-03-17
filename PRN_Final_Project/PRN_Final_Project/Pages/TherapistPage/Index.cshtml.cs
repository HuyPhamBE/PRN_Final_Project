using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;
using Services.Interface;

namespace PRN_Final_Project.Pages.TherapistPage
{
    public class IndexModel : PageModel
    {

        private readonly ITherapistService _therapistService;

        public IndexModel(ITherapistService therapistService)
        {
            _therapistService = therapistService;
        }

        public IList<Therapist> Therapist { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            var therapists = await _therapistService.GetAll();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                therapists = therapists
                    .Where(t => t.fullName.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            Therapist = therapists;
        }
    }
}
