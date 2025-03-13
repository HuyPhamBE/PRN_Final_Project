using Repositories.Model.Therapist;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class TherapistService : ITherapistService
    {
        public Task CreateTherapist(CreateTherapistModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTherapist(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TherapistServiceModel>> GetTherapistAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TherapistServiceModel> GetTherapistAsyncById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTherapist(UpdateTherapistModel model, string id)
        {
            throw new NotImplementedException();
        }
    }
}
