﻿using Entities.IUOW;
using Repositories.Model.Customer;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Repositories.Entities;
using Microsoft.EntityFrameworkCore;
namespace Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateCustomer(CreateCustomerModel model)
        {
           Customer customer=_mapper.Map<Customer>(model);
           customer.cusID=Guid.NewGuid();
            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.GetRepository<Customer>().InsertAsync(customer);
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                Console.WriteLine(ex.Message);            
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCustomer(string id)
        {
            var customer = await _unitOfWork.GetRepository<Customer>()
                 .Entities
                 .FirstOrDefaultAsync(c => c.cusID.ToString() == id);
            await _unitOfWork.GetRepository<Customer>().DeleteAsync(customer);
        }

        public async Task<CustomerServiceModel> GetCustomerAsyncById(string id)
        {
            Customer? customer = await _unitOfWork.GetRepository<Customer>()
                .Entities
                .FirstOrDefaultAsync(c => c.cusID.ToString() == id);
            if(customer != null)
            {
                return _mapper.Map<CustomerServiceModel>(customer);
            }
            else
            {
                throw new KeyNotFoundException("Customer does not exit");
            }
        }

        public async Task<IList<CustomerServiceModel>> GetCustomersAsync()
        {
            var customers=await _unitOfWork.GetRepository<Customer>().Entities
                .OrderByDescending(c => c.cusID)
                .Include(c=>c.accountID)
                .ToListAsync();
            return _mapper.Map<List<CustomerServiceModel>>(customers);
        }

        public async Task UpdateCustomer(UpdateCustomerModel model, string id)
        {
            var customer = await _unitOfWork.GetRepository<Customer>()
                .Entities
                .FirstOrDefaultAsync(c => c.cusID.ToString() == id);
            _mapper.Map(model, customer);
            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.GetRepository<Customer>().UpdateAsync(customer);
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                Console.WriteLine(ex.Message);
            }
            await  _unitOfWork.SaveAsync();
        }
    }
}
