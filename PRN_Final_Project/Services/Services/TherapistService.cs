using Entities.IUOW;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services
{
    public class TherapistService : ITherapistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<Account> _passwordHasher;

        public TherapistService(IUnitOfWork unitOfWork, IPasswordHasher<Account> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task AddNewTherapist(Therapist therapist)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<Therapist>();
                await repository.InsertAsync(therapist);
                await _unitOfWork.SaveAsync(); 
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding a new therapist.", ex);
            }
        }

        public async Task ToggleTherapistStatus(Guid id)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<Therapist>();
                var therapist = await repository.GetByIdAsync(id)
                                ?? throw new KeyNotFoundException($"Therapist with ID {id} not found.");

                therapist.status = therapist.status == "Deleted" ? "Active" : "Deleted";

                repository.UpdateAsync(therapist);
                await _unitOfWork.SaveAsync();
            }
            catch (KeyNotFoundException knfEx)
            {
                throw knfEx;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the therapist status.", ex);
            }
        }



        public async Task<IList<Therapist>> GetAll()
        {
            try
            {
                var repository = _unitOfWork.GetRepository<Therapist>();

                return await repository.Entities
                    .Include(t => t.Account)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving therapists.", ex);
            }
        }

        public async Task<Therapist?> GetTherapistById(Guid theraID)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<Therapist>();
                return await repository.GetByIdAsync(theraID)
                    ?? throw new KeyNotFoundException($"Therapist with ID {theraID} not found.");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving therapist details.", ex);
            }
        }

        public async Task UpdateTherapist(Therapist therapist)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<Therapist>();
                var existingTherapist = await repository.GetByIdAsync(therapist.theraID);

                if (existingTherapist == null)
                {
                    throw new KeyNotFoundException("Therapist not found.");
                }

                existingTherapist.fullName = therapist.fullName;
                existingTherapist.gender = therapist.gender;
                existingTherapist.major = therapist.major;
                existingTherapist.imageUrl = therapist.imageUrl;
                existingTherapist.exp = therapist.exp;
                existingTherapist.status = therapist.status;
                existingTherapist.updatedAt = DateTime.UtcNow;

                await repository.UpdateAsync(existingTherapist);
                Console.WriteLine("Update medicine successfully !");
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ThreadExceptionEventArgs args = new ThreadExceptionEventArgs(ex);
            }
        }

        public async Task<IList<Therapist>> SearchByName(string name)
        {
            try
            {
                var repository = _unitOfWork.GetRepository<Therapist>();

                return await repository.Entities
                    .Include(t => t.Account)
                    .Where(t => t.fullName.Contains(name))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving therapists.", ex);
            }
        }
        public async Task<Therapist> CreateTherapistWithAccountAsync(Account account, Therapist therapist)
        {
            var accountRepository = _unitOfWork.GetRepository<Account>();
            var therapistRepository = _unitOfWork.GetRepository<Therapist>();

            // Check if the username or email already exists
            var existingAccount = await accountRepository.FirstorDefaultAsync(acc => acc.userName == account.userName || acc.email == account.email);
            if (existingAccount != null)
            {
                throw new Exception("Username or Email already exists!");
            }
            Console.WriteLine(account.password);
  
            // Hash the password before saving
            account.password = _passwordHasher.HashPassword(account, account.password);

            // Save the account
            await accountRepository.InsertAsync(account);
            await _unitOfWork.SaveAsync(); // Ensure accountID is generated

            // Link the therapist to the created account
            therapist.accountID = account.accountID;

            // Save the therapist
            await therapistRepository.InsertAsync(therapist);
            await _unitOfWork.SaveAsync(); // Save therapist

            return therapist;
        }

        public async Task<bool> DeleteTherapistAsync(Guid id)
        {
            var repository = _unitOfWork.GetRepository<Therapist>();

            var therapist = await repository.GetByIdAsync(id);
            if (therapist == null)
            {
                throw new KeyNotFoundException($"Therapist with ID {id} not found.");
            }

            // Mark as inactive instead of deleting
            therapist.status = "inactive";

            await repository.UpdateAsync(therapist);
            await _unitOfWork.SaveAsync();

            return true;
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

        public async Task<Therapist> UpdateTherapistAsync(Guid theraId)
        {
            var repository = _unitOfWork.GetRepository<Therapist>();

            Therapist therapist = await repository.GetByIdAsync(theraId);

            if (therapist == null)
            {
                throw new KeyNotFoundException($"Therapist with ID {theraId} not found.");
            }

            
            repository.UpdateAsync(therapist);

            
            await _unitOfWork.SaveAsync();

            return therapist;

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
