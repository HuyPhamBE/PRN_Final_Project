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
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomerAsync()
        {
            return await _unitOfWork.GetRepository<Customer>().GetAllAsync(); 
        }

        public async Task<Customer?> GetCustomerByUserId(Guid id)
        {
            return await _unitOfWork.GetRepository<Customer>().Entities.Where(x => x.accountID == id).FirstOrDefaultAsync();
        }

        public async Task<int> GetTotalCustomers()
        {
            return await _unitOfWork.GetRepository<Customer>().Entities.CountAsync();
        }
    }
}
