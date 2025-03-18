using AutoMapper;
using Entities.IUOW;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.ServiceType;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ServiceTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateServiceType(CreateServiceTypeModel model)
        {
            ServiceType serviceType = mapper.Map<ServiceType>(model);
            await unitOfWork.GetRepository<ServiceType>().InsertAsync(serviceType);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteServiceType(string id)
        {
            await unitOfWork.GetRepository<ServiceType>().DeleteAsync(id);
            await unitOfWork.SaveAsync();
        }

        public async Task<IList<ServiceTypeServiceModel>> GetServiceTypeAsync()
        {
            var serviceTypes = await unitOfWork.GetRepository<ServiceType>().GetAllAsync();
            return mapper.Map<IList<ServiceTypeServiceModel>>(serviceTypes);
        }

        public async Task<ServiceTypeServiceModel> GetServiceTypeAsyncById(string id)
        {
            var serviceType = await unitOfWork.GetRepository<ServiceType>().GetByIdAsync(id);
            return mapper.Map<ServiceTypeServiceModel>(serviceType);
        }

        public async Task UpdateServiceType(UpdateServiceTypeModel model, string id)
        {
            var serviceType = mapper.Map<ServiceType>(model);
            await unitOfWork.GetRepository<ServiceType>().UpdateAsync(serviceType);
            await unitOfWork.SaveAsync();
        }
    }
}
