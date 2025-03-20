using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.IUOW;
using Repositories.Entities;
using Services.Interface;

namespace Services.Services
{
    public class TherapistService : ITherapistService
    {
        private readonly IUnitOfWork _unitOfWork;


        public TherapistService(IUnitOfWork unitOfWord)
        {
            _unitOfWork = unitOfWord;
        }

        public async Task<List<Therapist>> GetAllTherapists()
        {
            var repo = _unitOfWork.GetRepository<Therapist>();
            var allTherapists = await repo.GetAllAsync();
            if (allTherapists == null)
            {
                throw new Exception("There is no therapist at the moment!");
            }
            return (List<Therapist>)allTherapists;

        }
    }
}
