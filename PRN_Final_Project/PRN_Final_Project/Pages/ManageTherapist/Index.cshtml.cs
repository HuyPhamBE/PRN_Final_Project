using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Services.Interface;

namespace PRN_Final_Project.Pages.ManageTherapist
{
    public class IndexModel : PageModel
    {
        private readonly ITherapistService _therapistService;

        public IndexModel(ITherapistService therapistService)
        {
            _therapistService = therapistService;
        }

        public List<Therapist> Therapists { get; set; } = new List<Therapist>();

        public async Task OnGetAsync()
        {
            try
            {
                Therapists = await _therapistService.GetAllTherapists();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }
    }
}
