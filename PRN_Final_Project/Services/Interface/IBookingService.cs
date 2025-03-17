using Repositories.Model.Blog;
﻿using Repositories.Entities;
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
        Task<BookingServiceModel> GetBookingAsyncById(string id);
        Task<IList<BookingServiceModel>> GetBookingAsync();
        Task CreateBooking(CreateBookingModel model);
        Task DeleteBooking(string id);
        Task UpdateBooking(UpdateBookingModel model, string id);
        Task<IList<Booking>> GetByTherapistId(Guid therapistId);

        Task<Booking?> GetById(Guid id);
    }
}
