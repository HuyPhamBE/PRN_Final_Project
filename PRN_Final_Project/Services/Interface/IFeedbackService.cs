using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IFeedbackService
    {
        // Get all feedback
        Task<IEnumerable<Feedback>> GetAllFeedbackAsync();

        // Get feedback by ID
        Task<Feedback?> GetFeedbackByIdAsync(Guid feedbackId);

        // Get feedback by account ID
        Task<IEnumerable<Feedback>> GetFeedbackByAccountIdAsync(Guid accountId);

        // Get feedback related to a specific service
        Task<IEnumerable<Feedback>> GetFeedbackByServiceIdAsync(Guid serviceId);

        // Get feedback by booking ID (indirectly using service ID)
        Task<IEnumerable<Feedback>> GetFeedbackByBookingIdAsync(Guid bookingId);

        // Get recent feedback 
        Task<IEnumerable<Feedback>> GetRecentFeedbackAsync(int count);
    }
}
