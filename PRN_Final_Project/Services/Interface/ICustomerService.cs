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
        Task<CustomerServiceModel> GetCustomerAsyncById(string id);
        Task<IList<CustomerServiceModel>> GetCustomersAsync();
        Task CreateCustomer(CreateCustomerModel model);
        Task UpdateCustomer(UpdateCustomerModel model,string id);
        Task DeleteCustomer(string id);

    }
}
