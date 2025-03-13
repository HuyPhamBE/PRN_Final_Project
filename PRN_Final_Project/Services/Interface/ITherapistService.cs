using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ITherapistService
    {
        Task<IEnumerable<Therapist>> GetAllTherapistsAsync();

        Task<int> GetCountActiveTherapist();

        Task<Therapist> CreateTherapistAsync(Therapist therapist);

        Task<Therapist> UpdateTherapistAsync(Guid theraId);

        Task<Therapist> GetTherapistByTheraIdAsync(Guid theraId);

        Task<bool> DeleteTherapistAsync(int id);

        Task<bool> UpdateStatusActiveTheraAsync(int id);
    }
}
