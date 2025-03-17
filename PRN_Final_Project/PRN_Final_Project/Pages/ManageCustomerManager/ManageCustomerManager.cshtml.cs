using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Entities;
using Services.Interface;
using Services.Services;

namespace PRN_Final_Project.Pages.ManageCustomerManager
{
    public class ManageCustomerManagerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IBookingService _bookingService;

        public ManageCustomerManagerModel(ICustomerService customerService, IBookingService bookingService)
        {
            _customerService = customerService;
            _bookingService = bookingService;
        }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Booking> Bookings { get; set; } = new List<Booking>();

        [BindProperty(SupportsGet = true)]
        public Guid? CustomerId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Customers = (await _customerService.GetAllCustomerAsync()).ToList();

            // Fetch bookings for the selected customer
            if (CustomerId.HasValue)
            {
                Bookings = (await _bookingService.GetBookingsByCusIdAsync(CustomerId.Value)).ToList();
            }

            return Page();
        }
    }
}
