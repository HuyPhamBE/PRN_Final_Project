using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Repositories.Entities;
using Repositories.Model.Booking;
using Services.Interface;
using Services.Services;

namespace PRN_Final_Project.Pages.BookingPage
{
    public class ChooseServiceForBookingModel : PageModel
    {
        private readonly IServiceService serviceService;
        private readonly IBookingService bookingService;

        public ChooseServiceForBookingModel(IServiceService serviceService,
            IBookingService bookingService)
        {
            this.serviceService = serviceService;
            this.bookingService = bookingService;
        }
        [BindProperty]
        public CreateBookingModel BookingModel { get; set; }
        [BindProperty]
        public Guid SelectedServiceID { get; set; }
        public async Task<IActionResult> OnGet()
        {   
            if (TempData["BookingData"] != null)
            {
                BookingModel = JsonConvert.DeserializeObject<CreateBookingModel>((string)TempData["BookingData"]);
                // Keep TempData for the subsequent POST (if needed)
                TempData.Keep("BookingData");
            }
            else
            {
                // Handle missing booking data (e.g., redirect back to Create page)
                TempData["Notification"] = "Please fill in the required information before choosing a service.";
                return RedirectToPage("/BookingPage/Create");
            }
            // Update the booking model with the selected service
            BookingModel.serviceID = SelectedServiceID;

            var services = await serviceService.GetServiceAsync();
            ViewData["serviceID"] = new SelectList(services, "ServiceID", "description");

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            // Retrieve the booking data from TempData again
            if (TempData["BookingData"] != null)
            {
                BookingModel = JsonConvert.DeserializeObject<CreateBookingModel>((string)TempData["BookingData"]);
                TempData.Keep("BookingData");
            }
            else
            {
                TempData["Notification"] = "Please fill in the required information before choosing a service.";
                return RedirectToPage("Create");
            }

            // Update the booking model with the selected service
            BookingModel.serviceID = SelectedServiceID;

            BookingModel.total = (await serviceService.GetServiceAsyncById(SelectedServiceID)).price;
            TempData["BookingData"] = JsonConvert.SerializeObject(BookingModel);
            // Now add the booking record to the database with the complete information
            // (assuming bookingService.AddBooking handles the new booking correctly)
            await bookingService.AddBooking(BookingModel);

            // Redirect to a confirmation or booking list page
            return RedirectToPage("/PaymentPage/PaymentPage");
        }
    }
}
