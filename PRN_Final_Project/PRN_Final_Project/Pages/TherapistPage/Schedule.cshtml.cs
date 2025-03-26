using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Repositories.Entities;
using Services.Interface;
using System.Security.Claims;

namespace PRN_Final_Project.Pages.TherapistPage
{
    public class ScheduleModel : PageModel
    {
        private readonly ITherapistService _therapistService;
        private readonly IBookingService _bookingService;
        private readonly IAccountService _accountService;

        public ScheduleModel(ITherapistService therapistService, IBookingService bookingService, IAccountService accountService)
        {
            _therapistService = therapistService;
            _bookingService = bookingService;
            _accountService = accountService;
        }

        public IList<Booking> Bookings { get; set; } = new List<Booking>();

        //[BindProperty(SupportsGet = true)]
        //public Guid TherapistId { get; set; }  // Ensure this is always provided

        public async Task OnGetAsync()
        {
            try
            {
                // Get the current authenticated user's ID
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                {
                    TempData["Message"] = "Invalid or missing user ID.";
                    return;
                }

                // Fetch the therapist's details using the account ID
                var account = await _therapistService.GetTherapistByAccountId(userId);
                if (account == null)
                {
                    TempData["Message"] = "Therapist not found.";
                    return;
                }

                // Ensure theraID is valid
                if (!Guid.TryParse(account.theraID.ToString(), out var therapistGuid))
                {
                    TempData["Message"] = "Invalid therapist ID.";
                    return;
                }

                // Fetch bookings for the given therapist ID
                Bookings = await _bookingService.GetByTherapistId(therapistGuid) ?? new List<Booking>();

                if (!Bookings.Any())
                {
                    TempData["Message"] = "No bookings found for this therapist.";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An error occurred while retrieving bookings: " + ex.Message;
                Console.WriteLine($"Error retrieving bookings: {ex}");
            }
        }

    }
}
