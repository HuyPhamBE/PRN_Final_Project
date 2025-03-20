using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Entities;
using static Services.Services.BookingService;

namespace Services.Interface
{
    public interface IBookingService
    {
        Task<BookingsResponse> GetAllBookings(string searchTerm, int pageIndex, int pageSize);

        Task<bool> IsTherapistAvailableAsync(Guid therapistId, DateTime appointmentDay, Guid slotId);

        Task<Booking> GetBookingByID(Guid id);

        Task UpdateBooking(Booking booking);

    }
}
