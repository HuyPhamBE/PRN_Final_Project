using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Entities;
using Services.Interface;

namespace PRN_Final_Project.Pages.BookingPage
{
    public class BookingHistoryModel : PageModel
    {
        private readonly IBookingService bookingService;
        private readonly ICustomerService customerService;

        public BookingHistoryModel(IBookingService bookingService,
            ICustomerService customerService)
        {
            this.bookingService = bookingService;
            this.customerService = customerService;
        }
        public IList<Booking> Bookings { get; set; }
        public async Task OnGet()
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            Customer cus = await customerService.GetCustomerByUserId(Guid.Parse(userIdStr));
            Bookings = (await bookingService.GetBookingsByCusIdAsync(cus.cusID)).ToList();
        }
    }
}
