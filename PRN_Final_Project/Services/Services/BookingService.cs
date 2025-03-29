using Repositories.Model.Booking;
﻿using Entities.IUOW;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper mapper;

        public BookingService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
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

        public async Task<IEnumerable<Booking>> GetAllBookingCompleteAsync()
        {
            return await _unitOfWork.GetRepository<Booking>().Entities.Where(x => x.status == ("Completed")).ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetBookingsByCusIdAsync(Guid cusId)
        {
            return await _unitOfWork.GetRepository<Booking>().Entities.Where(x => x.cusID == cusId).ToListAsync();
        }

        public async Task<int> GetBookingsCountAsync()
        {
            return await _unitOfWork.GetRepository<Booking>().Entities.CountAsync();
        }

        public async Task<Dictionary<string, int>> GetBookingTrends()
        {
            var sixMonthsAgo = DateTime.UtcNow.AddMonths(-6);

            var bookings = await _unitOfWork.GetRepository<Booking>().Entities
                .Where(b => b.appointmentDay >= sixMonthsAgo)
                .ToListAsync(); 

            var groupedBookings = bookings
                .GroupBy(b => b.appointmentDay.ToString("MMM")) 
                .ToDictionary(g => g.Key, g => g.Count());

            return groupedBookings;
        }

        public async Task<Dictionary<string, decimal>> GetMonthlyRevenue()
        {
            var sixMonthsAgo = DateTime.Now.AddMonths(-6);

            return await _unitOfWork.GetRepository<Booking>().Entities
                .Where(b => b.appointmentDay >= sixMonthsAgo)
                .GroupBy(b => b.appointmentDay.ToString("MMM")) 
                .Select(g => new { Month = g.Key, Revenue = g.Sum(b => b.total) })
                .ToListAsync()
                .ContinueWith(task => task.Result.ToDictionary(g => g.Month, g => g.Revenue));

        }

        public async Task<IEnumerable<Booking>> GetRecentBookings(int count)
        {
            return await _unitOfWork.GetRepository<Booking>().Entities.OrderByDescending(b => b.createdAt).Take(count).Include(s => s.Service).Include(c => c.Customer).ToListAsync();
        }

        public async Task<decimal> getTotalRevenue()
        {
            return await _unitOfWork.GetRepository<Booking>().Entities.SumAsync(b => b.total);
        }

        public async Task AddBooking(CreateBookingModel model)
        {
            Booking booking=mapper.Map<Booking>(model);
            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.GetRepository<Booking>().InsertAsync(booking);
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                Console.WriteLine(ex.Message);
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateBooking(UpdateBookingModel model, string id)
        {
            var booking = await _unitOfWork.GetRepository<Booking>()
                 .Entities
                 .FirstOrDefaultAsync(c => c.BookingID.ToString() == id);
            mapper.Map(model, booking);
            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.GetRepository<Booking>().UpdateAsync(booking);
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                Console.WriteLine(ex.Message);
            }
            await _unitOfWork.SaveAsync();
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

        public async Task<bool> IsTherapistAvailableAsync(Guid? therapistId, DateTime appointmentDay, Guid slotId)
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
        public async Task<IList<Booking>> GetAll()
        {
            var bookings = await _unitOfWork.GetRepository<Booking>().Entities
                .Include(b => b.Customer)
                .Include(b => b.Service)
                .Include(b => b.Slot)
                .Include(b => b.Therapist).ToListAsync();
            return bookings
            ;
        }
    }
}
