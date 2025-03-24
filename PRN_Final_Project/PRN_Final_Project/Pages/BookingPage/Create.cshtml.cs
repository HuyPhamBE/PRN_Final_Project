using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.Booking;
using Services.Interface;

namespace PRN_Final_Project.Pages.BookingPage
{
    public class CreateModel : PageModel
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IBookingService bookingService;
        private readonly ICustomerService customerService;
        private readonly IServiceService serviceService;
        private readonly ISlotService slotService;
        private readonly ITherapistService therapistService;

        public CreateModel(IHttpContextAccessor httpContextAccessor,
            IBookingService bookingService,
            ICustomerService customerService,
            IServiceService serviceService,
            ISlotService slotService,
            ITherapistService therapistService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.bookingService = bookingService;
            this.customerService = customerService;
            this.serviceService = serviceService;
            this.slotService = slotService;
            this.therapistService = therapistService;
        }

        public async Task<IActionResult> OnGet()
        {
            //service option
            var services = await serviceService.GetServiceAsync();
            ViewData["serviceID"] = new SelectList(services, "ServiceID", "description");
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

        [BindProperty]
        public CreateBookingModel BookingModel { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var userIdString = httpContextAccessor.HttpContext.Request.Cookies["UserId"];
            var cusid = await customerService.GetCustomerByUserId(Guid.Parse(userIdString));
           BookingModel.cusID = cusid.cusID;
           await bookingService.AddBooking(BookingModel);

            return RedirectToPage("./Index");
        }
    }
}
