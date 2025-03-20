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
        Task<List<Therapist>> GetAllTherapists();
    }
}
