using AutoMapper;
using Entities.IUOW;
using Repositories.Entities;
using Repositories.Model.Service;
using Services.Interface;


namespace Services.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateService(CreateServiceModel model)
        {
            var service = mapper.Map<Service>(model);
            await unitOfWork.GetRepository<Service>().InsertAsync(service);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteService(string id)
        {
            await unitOfWork.GetRepository<Service>().DeleteAsync(id);
            await unitOfWork.SaveAsync();
        }

        public async Task<IList<ServiceServiceModel>> GetServiceAsync()
        {
            var services = await unitOfWork.GetRepository<Service>().GetAllAsync();
            return mapper.Map<IList<ServiceServiceModel>>(services);
        }
       
        public async Task<ServiceServiceModel> GetServiceAsyncById(string id)
        {
            var service = await unitOfWork.GetRepository<Service>().GetByIdAsync(id);
            return mapper.Map<ServiceServiceModel>(service);

        }

        public async Task UpdateService(UpdateServiceModel model, string id)
        {
            var ser=mapper.Map<Service>(model);
            await unitOfWork.GetRepository<Service>().UpdateAsync(ser);
            await unitOfWork.SaveAsync();
        }
    }
}
