using Entities.IUOW;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Booking?> GetById(Guid id)
        {
            var repository = _unitOfWork.GetRepository<Booking>();

            return await repository.Entities
                .Include(b => b.Customer) 
                .FirstOrDefaultAsync(b => b.BookingID == id);
        }

        public async Task<IList<Booking>> GetByTherapistId(Guid therapistId)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<Booking>();

                var bookings = await repository.Entities
                    .Where(b => b.theraID == therapistId)
                    .Include(b => b.Slot)
                    .Include(b => b.Customer)
                    .ToListAsync();

                if (!bookings.Any())
                {
                    return new List<Booking>(); 
                }

                return bookings;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving bookings for the therapist.", ex);
            }
        }

    }
}
