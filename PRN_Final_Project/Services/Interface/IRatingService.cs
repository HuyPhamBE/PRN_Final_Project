using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IRatingService
    {
        Task<IEnumerable<Rating>> GetAllRatingsAsync();
        Task<Rating?> GetRatingByIdAsync(Guid rateID);
        Task<IEnumerable<Rating>> GetRatingsByTherapistIdAsync(Guid theraID);

        Task<double> GetAverageRatingByTherapistIdAsync(Guid theraID);

        Task AddRatingAsync(Rating rating);

        Task<Dictionary<byte, int>> GetRatingCountsAsync();
    }
}
