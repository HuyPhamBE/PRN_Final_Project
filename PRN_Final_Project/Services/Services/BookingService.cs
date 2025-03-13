using Repositories.Model.Booking;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BookingService : IBookingService
    {
        public Task CreateBooking(CreateBookingModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBooking(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<BookingServiceModel>> GetBookingAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BookingServiceModel> GetBookingAsyncById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBooking(UpdateBookingModel model, string id)
        {
            throw new NotImplementedException();
        }
    }
}
