using Repositories.Model.TherapistResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ITherapistResultService
    {
        Task<TherapistResultServiceModel> GetTherapistResultAsyncById(string id);
        Task<IList<TherapistResultServiceModel>> GetTherapistResultAsync();
        Task CreateTherapistResult(CreateTherapistResultModel model);
        Task DeleteTherapistResult(string id);
        Task UpdateTherapistResult(UpdateTherapistResultModel model, string id);
    }
}
