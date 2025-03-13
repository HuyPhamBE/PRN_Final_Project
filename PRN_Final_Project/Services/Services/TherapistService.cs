using Entities.IUOW;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
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
        private readonly IUnitOfWork _unitOfWork;

        public TherapistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Therapist> CreateTherapistAsync(Therapist therapist)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTherapistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Therapist>> GetAllTherapistsAsync()
        {
            return await _unitOfWork.GetRepository<Therapist>().Entities.ToListAsync();
        }

        public async Task<int> GetCountActiveTherapist()
        {
           return await _unitOfWork.GetRepository<Therapist>().Entities.Where(x => x.status == "active").CountAsync();
        }

        public Task<Therapist> GetTherapistByTheraIdAsync(Guid theraId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateStatusActiveTheraAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Therapist> UpdateTherapistAsync(Guid theraId)
        {
            throw new NotImplementedException();
        }
    }
}
