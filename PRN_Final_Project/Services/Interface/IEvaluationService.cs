using Repositories.Model.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IEvaluationService
    {
        Task<EvaluationServiceModel> GetEvaluationAsyncById(string id);
        Task<IList<EvaluationServiceModel>> GetEvaluationAsync();
        Task CreateEvaluation(CreateEvaluationModel model);
        Task UpdateEvaluation(UpdateEvaluationModel model, string id);
    }
}
