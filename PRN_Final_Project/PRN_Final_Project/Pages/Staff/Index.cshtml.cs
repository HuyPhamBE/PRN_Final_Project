using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN_Assignment.Pages;
using Repositories.DB;
using Repositories.Entities;
using Services.Interface;

namespace PRN_Final_Project.Pages.Staff
{
    public class IndexModel : PageModel
    {
        private readonly IBookingService _bookingService;
        private readonly ITherapistService _therapistService;

        public IndexModel(IBookingService bookingService, ITherapistService therapistService)
        {
            _bookingService = bookingService;
            _therapistService = therapistService;
        }

        // Pagination properties
        [BindProperty(SupportsGet = true)]
        public string searchTerm { get; set; }

        //paging
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 4;

        public IList<Booking> Booking { get; set; } = default!;
        public IList<Therapist> Therapist { get; set; } = default!;

        public SelectList Therapists { get; set; }
        //public SelectList Therapists { get; set; }

        public async Task OnGetAsync(int pageIndex = 1)
        {
            var result = await _bookingService.GetAllBookings(searchTerm, pageIndex, PageSize);

            Console.WriteLine($"Retrieved {result.Bookings?.Count ?? 0} bookings");

            //Therapists = new SelectList(await _therapistService.GetAllTherapists(), "theraID", "fullName");



            Booking = result.Bookings;
            PageIndex = result.PageIndex;
            TotalPages = result.TotalPages;
        }

        // Add this to your IndexModel class
        public async Task<IActionResult> OnGetAvailableTherapistsAsync(Guid bookingId)
        {
            var booking = await _bookingService.GetBookingByID(bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            // Get all therapists
            var allTherapists = await _therapistService.GetAllTherapists();

            // Filter out busy therapists for this appointment day and slot
            var availableTherapists = new List<SelectListItem>();

            foreach (var therapist in allTherapists)
            {
                bool isAvailable = await _bookingService.IsTherapistAvailableAsync(
                    therapist.theraID,
                    booking.appointmentDay,
                    booking.slotID);

                if (isAvailable)
                {
                    availableTherapists.Add(new SelectListItem
                    {
                        Value = therapist.theraID.ToString(),
                        Text = therapist.fullName
                    });
                }
            }

            return new JsonResult(availableTherapists);
        }

        public async Task<IActionResult> OnPostCheckInAsync(Guid id)
        {
            var booking = await _bookingService.GetBookingByID(id);
            if (booking != null)
            {
                if (booking.theraID == null)
                {
                    booking.status = "checkedIn";
                }
                else
                {
                    booking.status = "inProgress";
                }

                booking.updatedAt = DateTime.UtcNow;
                await _bookingService.UpdateBooking(booking);
            }
            return RedirectToPage();
        }

        // Assign Specialist Handler
        public async Task<IActionResult> OnPostAssignSpecialistAsync(Guid bookingId, Guid therapistId)
        {
            var booking = await _bookingService.GetBookingByID(bookingId);

            if (therapistId == null)
            {
                TempData["Error"] = "Please select a specialist to assign.";
                return RedirectToPage();
            }

            if (booking != null)
            {
                booking.theraID = therapistId;
                booking.status = "inProgress";
                booking.updatedAt = DateTime.UtcNow;
                await _bookingService.UpdateBooking(booking);
                TempData["Success"] = "Specialist assigned!";
            }
            return RedirectToPage();
        }

        // Check-Out Handler
        public async Task<IActionResult> OnPostCheckOutAsync(Guid id)
        {
            var booking = await _bookingService.GetBookingByID(id);
            if (booking != null)
            {
                booking.status = "completed";
                booking.updatedAt = DateTime.UtcNow;
                await _bookingService.UpdateBooking(booking);
            }
            return RedirectToPage();
        }
    }

}
