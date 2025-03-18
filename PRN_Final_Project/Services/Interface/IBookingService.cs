using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Model.Booking;
namespace Services.Interface
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllBookingCompleteAsync();

        Task<IEnumerable<Booking>> GetBookingsByCusIdAsync(Guid cusId);

        Task<int> GetBookingsCountAsync();

        Task<IEnumerable<Booking>> GetRecentBookings(int count);

        Task<Dictionary<string, int>> GetBookingTrends();
        Task<IList<Booking>> GetByTherapistId(Guid therapistId);
        Task<Dictionary<string, decimal>> GetMonthlyRevenue();
        Task<Booking?> GetById(Guid id);
        Task<Decimal> getTotalRevenue();
    }
}
