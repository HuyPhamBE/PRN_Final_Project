using AutoMapper;
using Repositories.Entities;
using Repositories.Model.Blog;
using Repositories.Model.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Mapper
{
    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<CreateBlogModel, Blog>().ReverseMap();
            CreateMap<UpdateBlogModel, Blog>().ReverseMap();
            CreateMap<BlogServiceModel, Blog>().ReverseMap();
        }
    }
}
