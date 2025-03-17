using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interface;
using System;
using System.Collections.Generic;

namespace PRN_Final_Project.Pages.DashboardAndReport
{
    public class DashboardAndReportModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IBookingService _bookingService;
        private readonly IRatingService _ratingService;
        private readonly IFeedbackService _feedbackService;

        public DashboardAndReportModel(ICustomerService customerService, IBookingService bookingService, IRatingService ratingService, IFeedbackService feedbackService)
        {
            _customerService = customerService;
            _bookingService = bookingService;
            _ratingService = ratingService;
            _feedbackService = feedbackService;
        }

        public int TotalCustomers { get; set; }
        public int TotalBookings { get; set; }
        public int ActiveTherapists { get; set; }
        public decimal TotalRevenue { get; set; }

        public int countBooking { get; set; }

        public int countFeedback { get; set; }

        public List<string> BookingMonths { get; set; } = new();
        public List<int> BookingCounts { get; set; } = new();

        public List<int> RatingCounts { get; set; } = new();

        public List<(string CustomerName, string ServiceName, DateTime Date)> RecentBookings { get; set; } = new();
        public List<(string CustomerName, string ServiceName, string content)> CustomerFeedbacks { get; set; } = new();

        public async Task OnGetAsync(int? countBooking = 5, int? countFeedback = 5)
        {
            this.countBooking = countBooking ?? 5;
            this.countFeedback = countFeedback ?? 5;

            // Get total customers
            TotalCustomers = await _customerService.GetTotalCustomers();

            // Get total bookings
            TotalBookings = await _bookingService.GetBookingsCountAsync();

            // Get total revenue
            TotalRevenue = await _bookingService.getTotalRevenue();

            // Get booking trends (last 6 months)
            var bookingTrends = await _bookingService.GetBookingTrends();
            BookingMonths = bookingTrends.Keys.ToList();
            BookingCounts = bookingTrends.Values.ToList();

            // Get rating counts (1-5 stars)
            var ratingCounts = await _ratingService.GetRatingCountsAsync();
            RatingCounts = ratingCounts.Values.ToList();

            // Get recent bookings
            var recentBookings = await _bookingService.GetRecentBookings(this.countBooking);
            RecentBookings = recentBookings.Select(b => (b.Customer.fullName, b.Service.serviceName, b.appointmentDay)).ToList();

            // Get recent customer feedback
            var feedbacks = await _feedbackService.GetRecentFeedbackAsync(this.countFeedback);
            CustomerFeedbacks = feedbacks
                .Select(f =>
                    (f.Account.Customer.FirstOrDefault()?.fullName ?? "Unknown", 
                     f.Service.serviceName,  
                     f.content)) 
                .ToList();
        }
    }
}
