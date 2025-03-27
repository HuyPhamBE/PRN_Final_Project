using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Model.Customer;
namespace Services.Interface
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByUserId(Guid id);

        Task<IEnumerable<Customer>> GetAllCustomerAsync();

        Task<int> GetTotalCustomers();

        Task CreateCustomer(CreateCustomerModel model);
        Task<CustomerServiceModel> GetCustomerAsyncById(string id);
        Task UpdateCustomer(UpdateCustomerModel model, Guid id);
    }
}
