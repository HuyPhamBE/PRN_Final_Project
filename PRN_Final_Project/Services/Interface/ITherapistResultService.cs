using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ITherapistResultService
    {
        Task<IList<TherapyResult>> GetAll();

        Task<TherapyResult> GetTherapyResultById(Guid therapyResultId);

        Task AddNewTherapytResult(TherapyResult therapyResult);

        Task UpdateTherapyResult(TherapyResult therapyResult);

        Task ToggleTherapyResultStatus(Guid id);  // soft delete
    }
}
