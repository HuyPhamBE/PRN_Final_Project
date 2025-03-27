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
using Services.Interface;
using Services.Services;

namespace PRN_Final_Project.Pages.BookingPage
{
    public class EditModel : PageModel
    {
        private readonly Repositories.DB.ApplicationDbContext _context;
        private readonly IBookingService bookingService;
        private readonly ISlotService slotService;
        private readonly ITherapistService therapistService;
        private readonly IServiceService serviceService;

        public EditModel(Repositories.DB.ApplicationDbContext context,
            IBookingService bookingService,
            ISlotService slotService,
            ITherapistService therapistService,
            IServiceService serviceService)
        {
            _context = context;
            this.bookingService = bookingService;
            this.slotService = slotService;
            this.therapistService = therapistService;
            this.serviceService = serviceService;
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await bookingService.GetBookingByID(id);
            if (booking == null)
            {
                return NotFound();
            }
            Booking = booking;
            // service options
            var services = await serviceService.GetServiceAsync();
            var serviceListItem= services.Select(s => new SelectListItem
            {
                Value = s.ServiceID.ToString(),
                Text = s.description
            }).ToList();
            ViewData["serviceID"] = serviceListItem;
            //slot options
            var slots = await slotService.GetSlotAsync();
            var slotSelectListItems = slots.Select(s => new SelectListItem
            {
                Value = s.SlotID.ToString(),
                Text = $"{s.startTime.ToString(@"hh\:mm")}-{s.endTime.ToString(@"hh\:mm")}"
            }).ToList();

            ViewData["slotID"] = new SelectList(slotSelectListItems, "Value", "Text");
            // therapist option
            var therapists = await therapistService.GetAllTherapistsAsync();
            ViewData["theraID"] = new SelectList(therapists, "theraID", "fullName");
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

           await bookingService.UpdateBooking(Booking);

            return RedirectToPage("./Index");
        }
    }
}
