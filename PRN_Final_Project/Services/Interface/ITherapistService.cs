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
        Task<IList<Therapist>> GetAll();

        Task<Therapist> GetTherapistById(Guid theraID);

        Task AddNewTherapist(Therapist therapist);

        Task UpdateTherapist(Therapist therapist);

        Task ToggleTherapistStatus(Guid id);  // soft delete
        Task<IList<Therapist>> SearchByName(string fullName);

    }
}
