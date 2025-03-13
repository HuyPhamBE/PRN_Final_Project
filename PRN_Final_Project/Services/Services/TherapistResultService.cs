using Repositories.Model.TherapistResult;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class TherapistResultService : ITherapistResultService
    {
        public Task CreateTherapistResult(CreateTherapistResultModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTherapistResult(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TherapistResultServiceModel>> GetTherapistResultAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TherapistResultServiceModel> GetTherapistResultAsyncById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTherapistResult(UpdateTherapistResultModel model, string id)
        {
            throw new NotImplementedException();
        }
    }
}
