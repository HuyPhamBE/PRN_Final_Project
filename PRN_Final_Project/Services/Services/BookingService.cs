using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.IUOW;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Services.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;


        public BookingService(IUnitOfWork unitOfWord)
        {
            _unitOfWork = unitOfWord;
        }

        public class BookingsResponse
        {
            public List<Booking> Bookings { get; set; }
            public int TotalPages { get; set; }
            public int PageIndex { get; set; }
        }

        public async Task<BookingsResponse> GetAllBookings(string searchTerm, int pageIndex, int pageSize)
        {
            var repo = _unitOfWork.GetRepository<Booking>();

            // Retrieve bookings with related properties
            //var bookings = await repo.GetAllAsync(b => b.Therapist, b => b.Service, b => b.Customer);

            var bookings = await repo.Entities.Include(a => a.Therapist).Include(b => b.Service).Include(c => c.Customer).Include(d => d.Slot).ToListAsync();


            // Convert the list to an IQueryable for filtering and pagination
            IQueryable<Booking> query = bookings.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // Use case-insensitive search for customer name, service name, and specialist name
                string lowerSearchTerm = searchTerm.ToLower();
                query = query.Where(x => x.Customer.fullName.ToLower().Contains(lowerSearchTerm) ||
                                         x.Service.serviceName.ToLower().Contains(lowerSearchTerm) ||
                                         (x.Therapist != null && x.Therapist.fullName.ToLower().Contains(lowerSearchTerm)));
            }

            // Use synchronous Count since query is in-memory
            int count = query.Count();
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            // Apply pagination: Order, skip, and take the pageSize number of items.
            query = query.OrderByDescending(x => x.createdAt)
                         .Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize);

            return new BookingsResponse
            {
                Bookings = query.ToList(),
                TotalPages = totalPages,
                PageIndex = pageIndex
            };
        }

        public async Task<bool> IsTherapistAvailableAsync(Guid therapistId, DateTime appointmentDay, Guid slotId)
        {
            var repo = _unitOfWork.GetRepository<Booking>();

            var existingBooking = await repo.Entities
                .AnyAsync(b => b.theraID == therapistId
                          && b.appointmentDay.Date == appointmentDay.Date
                          && b.slotID == slotId
                          && b.status != "cancelled" && b.status != "completed");

            // Return true if no bookings found (therapist is available)
            return !existingBooking;
        }


        public async Task<Booking> GetBookingByID(Guid id)
        {
            var repo = _unitOfWork.GetRepository<Booking>();

            //var isExist = await repo.GetAllAsync(b => b.Therapist, b => b.Service, b => b.Customer, b => b.Slot);

            var isExist = await repo.Entities.Include(b => b.Therapist).Include(b => b.Service).Include(b => b.Customer).Include(b => b.Slot).ToListAsync();

            var finded = isExist.FirstOrDefault(b => b.BookingID == id);

            if (finded == null)
            {
                throw new Exception("There is no bookings!");
            }
            return finded;

        }

        public async Task UpdateBooking(Booking booking)
        {
            var repo = _unitOfWork.GetRepository<Booking>();
            var theraRepo = _unitOfWork.GetRepository<Therapist>();

            try
            {
                var isExist = await repo.GetByIdAsync(booking.BookingID);

                if (isExist == null)
                {
                    throw new Exception("There is no bookings!");
                }

                isExist.status = booking.status;
                isExist.updatedAt = booking.updatedAt;

                var therapist = await theraRepo.FirstorDefaultAsync(s => s.theraID == booking.theraID);
                //if (therapist == null)
                //{
                //    throw new Exception("therapist does not exist");
                //}
                isExist.theraID = booking.theraID;

                repo.UpdateAsync(isExist);
                await repo.SaveAsync();
            }
            catch (Exception e)
            {

                throw e;
            }


        }

    }
}
