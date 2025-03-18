using AutoMapper;
using Repositories.Entities;
using Repositories.Model.ServiceType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Mapper
{
    public class ServiceTypeMapper : Profile
    {
        public ServiceTypeMapper()
        {
            CreateMap<CreateServiceTypeModel, ServiceType>().ReverseMap();
            CreateMap<UpdateServiceTypeModel, ServiceType>().ReverseMap();
            CreateMap<ServiceTypeServiceModel, ServiceType>().ReverseMap();
        }
    }
}
