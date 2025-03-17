using Entities.IUOW;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity; 


namespace Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<Account> _passwordHasher;

        public AccountService(IUnitOfWork unitOfWork, IPasswordHasher<Account> passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<Account> register(Account account, Customer customer)
        {
            var repository = _unitOfWork.GetRepository<Account>();
            var customerRepository = _unitOfWork.GetRepository<Customer>();

            var existingAccount = await repository.FirstorDefaultAsync(acc => acc.userName == account.userName || acc.email == account.email);
            if (existingAccount != null)
            {
                throw new Exception("Username or Email already exists!");
            }

            account.password = _passwordHasher.HashPassword(account, account.password);

            await repository.InsertAsync(account);
            await customerRepository.InsertAsync(customer);
            await _unitOfWork.SaveAsync();

            return account;
        }


        public async Task<Account> Login(string username, string password)
        {
            var repository = _unitOfWork.GetRepository<Account>();

            var user = await repository.FirstorDefaultAsync(acc => acc.userName == username);
            if (user == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.password, password);

            return result == PasswordVerificationResult.Success ? user : null;

        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<Account>> GetAllAccount()
        {
            try
            {
                var repository = _unitOfWork.GetRepository<Account>();

                return await repository.Entities
                    .Where(t => t.status == "active")
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving therapists.", ex);
            }
        public Task<Account> getAccountByCusId(Guid cusId)
        {
            throw new NotImplementedException();
        }
    }
}
