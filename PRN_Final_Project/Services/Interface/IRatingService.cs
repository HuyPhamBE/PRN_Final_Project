using Repositories.Model.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IRatingService
    {
        Task<RatingServiceModel> GetRatingAsyncById(string id);
        Task<IList<RatingServiceModel>> GetRatingAsync();
        Task CreateRating(CreateRatingModel model);
        Task DeleteRating(string id);
        Task UpdateRating(UpdateRatingModel model, string id);
    }
}
