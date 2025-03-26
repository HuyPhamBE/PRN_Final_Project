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
    public class TherapistResultService : ITherapistResultService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TherapistResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddNewTherapytResult(TherapyResult therapyResult)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<TherapyResult>();
                var therapistByBookingId = await repository.GetByIdAsync(therapyResult.bookingID);
                if (therapistByBookingId != null)
                {
                    throw new Exception("Can not add new theraphy result for this booking");
                }
                await repository.InsertAsync(therapyResult);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding a new therapy result.", ex);
            }
        }


        public async Task<IList<TherapyResult>> GetAll()
        {
            try
            {
                var repository = _unitOfWork.GetRepository<TherapyResult>();

                return await repository.Entities
                    .Include(t => t.Booking) 
                    .ThenInclude(b => b.Customer) 
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving therapy results.", ex);
            }
        }


        public async Task<TherapyResult?> GetTherapyResultById(Guid therapyResultId)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<TherapyResult>();
                return await repository.GetByIdAsync(therapyResultId)
                    ?? throw new KeyNotFoundException($"Therapy result with ID {therapyResultId} not found.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving therapy result details.", ex);
            }
        }

        public async Task UpdateTherapyResult(TherapyResult therapyResult)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<TherapyResult>();
                var existingTherapyResult = await repository.GetByIdAsync(therapyResult.theraResultID);

                if (existingTherapyResult == null)
                {
                    throw new KeyNotFoundException("Therapy result not found.");
                }

                existingTherapyResult.content = therapyResult.content;
                existingTherapyResult.status = therapyResult.status;
                existingTherapyResult.updatedAt = DateTime.UtcNow;

                await repository.UpdateAsync(existingTherapyResult);
                Console.WriteLine("Update therapy result successfully !");
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ThreadExceptionEventArgs args = new ThreadExceptionEventArgs(ex);
            }
        }


        public async Task ToggleTherapyResultStatus(Guid id)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<TherapyResult>();
                var therapyResult = await repository.GetByIdAsync(id)
                                ?? throw new KeyNotFoundException($"Therapist with ID {id} not found.");

                therapyResult.status = therapyResult.status == "Deleted" ? "Active" : "Deleted";

                repository.UpdateAsync(therapyResult);
                await _unitOfWork.SaveAsync();
            }
            catch (KeyNotFoundException knfEx)
            {
                throw knfEx;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the therapy result status.", ex);
            }
        }

        public async Task<IList<TherapyResult>> GetAllById(Guid therapyResultId)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<TherapyResult>();

                return await repository.Entities
                    .Include(t => t.Booking)
                    .ThenInclude(b => b.Customer)
                    .Where(t => t.theraResultID == therapyResultId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving therapy results.", ex);
            }
        }
    }
}
