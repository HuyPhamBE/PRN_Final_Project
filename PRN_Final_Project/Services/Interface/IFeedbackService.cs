using Repositories.Model.Feedback;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IFeedbackService
    {
        Task<FeedbackServiceModel> GetFeedbackAsyncById(string id);
        Task<IList<FeedbackServiceModel>> GetFeedbackAsync();
        Task CreateFeedback(CreateFeedbackModel model);
        Task DeleteFeedback(string id);
        Task UpdateFeedback(UpdateFeedbackModel model, string id);
    }
}
