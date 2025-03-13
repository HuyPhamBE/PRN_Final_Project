using Repositories.Model.Rating;
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
        public Task CreateRating(CreateRatingModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRating(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<RatingServiceModel>> GetRatingAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RatingServiceModel> GetRatingAsyncById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRating(UpdateRatingModel model, string id)
        {
            throw new NotImplementedException();
        }
    }
}
