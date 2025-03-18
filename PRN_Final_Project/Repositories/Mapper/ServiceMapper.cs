using AutoMapper;
using Repositories.Entities;
using Repositories.Model.Service;

namespace Repositories.Mapper
{
    public class ServiceMapper:Profile
    {
        public ServiceMapper() 
        {
            CreateMap<CreateServiceModel, Service>().ReverseMap();
            CreateMap<UpdateServiceModel, Service>().ReverseMap();
            CreateMap<ServiceServiceModel, Service>().ReverseMap();
        }
    }
}
