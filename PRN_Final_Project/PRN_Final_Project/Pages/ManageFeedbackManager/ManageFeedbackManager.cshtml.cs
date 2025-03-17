using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN_Final_Project.Pages.ManageFeedbackManager
{
    public class ManageFeedbackManagerModel : PageModel
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IBookingService _bookingService;
        private readonly ICustomerService _customerService;

        public ManageFeedbackManagerModel(IFeedbackService feedbackService, IBookingService bookingService, ICustomerService customerService)
        {
            _feedbackService = feedbackService;
            _bookingService = bookingService;
            _customerService = customerService;
        }

        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public List<FeedbackViewModel> Feedbacks { get; set; } = new List<FeedbackViewModel>();

        [BindProperty(SupportsGet = true)]
        public Guid? BookingId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Fetch all completed bookings
            Bookings = (await _bookingService.GetAllBookingCompleteAsync()).ToList();

            // Fetch feedback for the selected booking
            if (BookingId.HasValue)
            {
                var feedbackList = await _feedbackService.GetFeedbackByBookingIdAsync(BookingId.Value);
                Feedbacks = (await Task.WhenAll(feedbackList.Select(async feedback =>
                {
                    var customer = await _customerService.GetCustomerByUserId(feedback.accountID);
                    return new FeedbackViewModel
                    {
                        FeedbackID = feedback.FeedbackID,
                        Content = feedback.content,
                        ServiceName = feedback.Service?.serviceName ?? "Unknown",
                        CustomerName = customer?.fullName ?? "Unknown"
                    };
                }))).ToList();
            }

            return Page();
        }

        public class FeedbackViewModel
        {
            public Guid FeedbackID { get; set; }
            public string Content { get; set; }
            public string ServiceName { get; set; }
            public string CustomerName { get; set; }
        }
    }
}
