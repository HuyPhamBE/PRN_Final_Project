using Repositories.Model.Rating;
﻿using Entities.IUOW;
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
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RatingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddRatingAsync(Rating rating)
        {
            await _unitOfWork.GetRepository<Rating>().InsertAsync(rating);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Rating>> GetAllRatingsAsync()
        {
            return await _unitOfWork.GetRepository<Rating>().GetAllAsync();
        }

        public async Task<double> GetAverageRatingByTherapistIdAsync(Guid theraID)
        {
            var ratings = await _unitOfWork.GetRepository<Rating>()
        .Entities.Where(r => r.theraID == theraID)
        .Select(r => (double)r.rates) 
        .ToListAsync();

            if (!ratings.Any())
                return 0;

            return ratings.Average();
        }

        public async Task<Rating?> GetRatingByIdAsync(Guid rateID)
        {
            return await _unitOfWork.GetRepository<Rating>().GetByIdAsync(rateID);
        }

        public async Task<Dictionary<byte, int>> GetRatingCountsAsync()
        {
            return await _unitOfWork.GetRepository<Rating>().Entities
                         .GroupBy(r => r.rates)
                         .Select(g => new { Rating = g.Key, Count = g.Count() })
                         .ToListAsync() 
                         .ContinueWith(task => task.Result.ToDictionary(g => g.Rating, g => g.Count));
        }

        public async Task<IEnumerable<Rating>> GetRatingsByTherapistIdAsync(Guid theraID)
        {
            return await _unitOfWork.GetRepository<Rating>().Entities
               .Where(r => r.theraID == theraID)
               .ToListAsync();
        }
    }
}
