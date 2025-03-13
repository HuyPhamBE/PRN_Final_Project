using Repositories.Model.Service;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ServiceService : IServiceService
    {
        public Task CreateService(CreateServiceModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteService(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ServiceServiceModel>> GetServiceAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceServiceModel> GetServiceAsyncById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateService(UpdateServiceModel model, string id)
        {
            throw new NotImplementedException();
        }
    }
}
