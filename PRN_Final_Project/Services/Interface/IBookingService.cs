using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Model.Booking;
using Repositories.Entities;
using static Services.Services.BookingService;

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
        Task AddBooking(CreateBookingModel model);
        Task UpdateBooking(UpdateBookingModel model, string id);
        Task<BookingsResponse> GetAllBookings(string searchTerm, int pageIndex, int pageSize);

        Task<bool> IsTherapistAvailableAsync(Guid? therapistId, DateTime appointmentDay, Guid slotId);

        Task<Booking> GetBookingByID(Guid id);

        Task UpdateBooking(Booking booking);
        Task<IList<Booking>> GetAll();

    }
}
