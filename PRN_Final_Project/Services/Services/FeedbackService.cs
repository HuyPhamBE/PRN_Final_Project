using Repositories.Model.Feedback;
﻿using Entities.IUOW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Repositories.Entities;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbackAsync()
        {
            return await _unitOfWork.GetRepository<Feedback>().GetAllAsync();
        }

        public async Task<IEnumerable<Feedback>> GetFeedbackByAccountIdAsync(Guid accountId)
        {
            return await _unitOfWork.GetRepository<Feedback>().Entities.Where(r => r.accountID == accountId).ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetFeedbackByBookingIdAsync(Guid bookingId)
        {
            var booking = await _unitOfWork.GetRepository<Booking>().GetByIdAsync(bookingId);

            if (booking != null)
            {
                var customer =  await _unitOfWork.GetRepository<Customer>().Entities.Where(x => x.cusID == booking.cusID).FirstOrDefaultAsync();

                return await _unitOfWork.GetRepository<Feedback>().Entities.Where(r => r.serviceID == booking.serviceID && r.accountID == customer.accountID).Include(f => f.Service).ToListAsync();
            }
            return await _unitOfWork.GetRepository<Feedback>().GetAllAsync();
        }
        public async Task<Feedback?> GetFeedbackByIdAsync(Guid feedbackId)
        {
            return await _unitOfWork.GetRepository<Feedback>().GetByIdAsync(feedbackId);
        }

        public async Task<IEnumerable<Feedback>> GetFeedbackByServiceIdAsync(Guid serviceId)
        {
            return await _unitOfWork.GetRepository<Feedback>().Entities.Where(r => r.serviceID == serviceId).ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetRecentFeedbackAsync(int count)
        {
            return await _unitOfWork.GetRepository<Feedback>().Entities.OrderByDescending(r => r.createdAt).Take(count).Include(a => a.Account).ThenInclude(a => a.Customer).Include(s => s.Service).ToListAsync();
        }
    }
}
