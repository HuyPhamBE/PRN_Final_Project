using Repositories.Model.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IServiceService
    {
        Task<ServiceServiceModel> GetServiceAsyncById(Guid id);
        Task<IList<ServiceServiceModel>> GetServiceAsync();
        Task CreateService(CreateServiceModel model);
        Task DeleteService(string id);
        Task UpdateService(UpdateServiceModel model, string id);
    }
}
