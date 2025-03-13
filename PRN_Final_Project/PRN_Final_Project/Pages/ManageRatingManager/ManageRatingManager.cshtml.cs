using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN_Final_Project.Pages.ManageRatingManager
{
    public class ManageRatingManagerModel : PageModel
    {
        private readonly IRatingService _ratingService;
        private readonly ITherapistService _therapistService;

        public ManageRatingManagerModel(IRatingService ratingService, ITherapistService therapistService)
        {
            _ratingService = ratingService;
            _therapistService = therapistService;
        }

        // Properties to store the data for the page
        public List<Therapist> Therapists { get; set; } = new List<Therapist>();
        public double AverageRating { get; set; }
        public List<Rating> Ratings { get; set; } = new List<Rating>();

        [BindProperty(SupportsGet = true)]
        public Guid? TherapistId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? TherapistName { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Get all therapists
            Therapists = (await _therapistService.GetAllTherapistsAsync()).ToList();

            if (TherapistId.HasValue)
            {
                Ratings = (await _ratingService.GetRatingsByTherapistIdAsync(TherapistId.Value)).ToList();
                AverageRating = await _ratingService.GetAverageRatingByTherapistIdAsync(TherapistId.Value);

                var therapist = Therapists.FirstOrDefault(t => t.theraID == TherapistId.Value);
                TherapistName = therapist?.fullName; // Set the therapist name
            }

            return Page();
        }
    }
}