using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Entities;
using Services.Interface;

namespace PRN_Final_Project.Pages.TherapistPage
{
    public class ScheduleModel : PageModel
    {
        private readonly ITherapistService _therapistService;
        private readonly IBookingService _bookingService;

        public ScheduleModel(ITherapistService therapistService, IBookingService bookingService)
        {
            _therapistService = therapistService;
            _bookingService = bookingService;
        }

        public IList<Therapist> Therapists { get; set; } = new List<Therapist>();
        public IList<Booking> Bookings { get; set; } = new List<Booking>();

        [BindProperty(SupportsGet = true)]
        public Guid? SelectedTherapistId { get; set; }

        public async Task OnGetAsync()
        {
            Therapists = await _therapistService.GetAll();

            if (SelectedTherapistId.HasValue)
            {
                try
                {
                    Bookings = await _bookingService.GetByTherapistId(SelectedTherapistId.Value);

                    if (!Bookings.Any())
                    {
                        TempData["Message"] = "No bookings found for this therapist.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "An error occurred while retrieving bookings: " + ex.Message;
                }
            }
        }
    }
}
