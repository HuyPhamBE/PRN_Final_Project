using Repositories.Model.Therapist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ITherapistService
    {
        Task<TherapistServiceModel> GetTherapistAsyncById(string id);
        Task<IList<TherapistServiceModel>> GetTherapistAsync();
        Task CreateTherapist(CreateTherapistModel model);
        Task DeleteTherapist(string id);
        Task UpdateTherapist(UpdateTherapistModel model, string id);
    }
}
