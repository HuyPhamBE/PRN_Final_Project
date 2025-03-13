using Repositories.Model.ServiceType;
using ServiceTypes.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        public Task CreateServiceType(CreateServiceTypeModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteServiceType(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ServiceTypeServiceModel>> GetServiceTypeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceTypeServiceModel> GetServiceTypeAsyncById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateServiceType(UpdateServiceTypeModel model, string id)
        {
            throw new NotImplementedException();
        }
    }
}
