﻿using Repositories.Model.Feedback;
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
        public Task CreateFeedback(CreateFeedbackModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFeedback(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<FeedbackServiceModel>> GetFeedbackAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FeedbackServiceModel> GetFeedbackAsyncById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFeedback(UpdateFeedbackModel model, string id)
        {
            throw new NotImplementedException();
        }
    }
}
