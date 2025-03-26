using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Entities;

namespace Services.Interface
{
    public interface ITherapistService
    {
        Task<IEnumerable<Therapist>> GetAllTherapistsAsync();
        Task<IList<Therapist>> GetAll();
        Task<int> GetCountActiveTherapist();
        Task<Therapist?> GetTherapistById(Guid theraID);
        Task<Therapist> CreateTherapistAsync(Therapist therapist);

        Task<Therapist> UpdateTherapistAsync(Guid theraId);

        Task<Therapist> GetTherapistByTheraIdAsync(Guid theraId);

        Task<bool> DeleteTherapistAsync(int id);

        Task<bool> UpdateStatusActiveTheraAsync(int id);
        Task ToggleTherapistStatus(Guid id);
        Task UpdateTherapist(Therapist therapist);
        Task<List<Therapist>> GetAllTherapists();
        Task<Therapist> GetTherapistByAccountId(Guid accountId);

    }
}
