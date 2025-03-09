using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Repositories.Entities;
using Repositories.Model.Customer;
namespace Repositories.Mapper
{
    public class CustomerMapper : Profile
    {
        public CustomerMapper() 
        {
            CreateMap<CreateCustomerModel, Customer>().ReverseMap();
            CreateMap<UpdateCustomerModel, Customer>().ReverseMap();
            CreateMap<CustomerServiceModel,Customer>().ReverseMap();
        }
    }
}
