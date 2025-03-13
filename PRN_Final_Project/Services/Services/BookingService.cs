using Entities.IUOW;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
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
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
