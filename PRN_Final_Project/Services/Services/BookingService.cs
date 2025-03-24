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
            booking.BookingID = Guid.NewGuid();
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
    }
}
