using Repositories.Model.ServiceType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTypes.Interface
{
    public interface IServiceTypeService
    {
        Task<ServiceTypeServiceModel> GetServiceTypeAsyncById(string id);
        Task<IList<ServiceTypeServiceModel>> GetServiceTypeAsync();
        Task CreateServiceType(CreateServiceTypeModel model);
        Task DeleteServiceType(string id);
        Task UpdateServiceType(UpdateServiceTypeModel model, string id);
    }
}
