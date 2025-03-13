using Repositories.Model.Evaluation;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class EvaluationService : IEvaluationService
    {
        public Task CreateEvaluation(CreateEvaluationModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IList<EvaluationServiceModel>> GetEvaluationAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EvaluationServiceModel> GetEvaluationAsyncById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEvaluation(UpdateEvaluationModel model, string id)
        {
            throw new NotImplementedException();
        }
    }
}
