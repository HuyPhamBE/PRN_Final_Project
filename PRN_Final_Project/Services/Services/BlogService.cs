using AutoMapper;
using Entities.IUOW;
using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using Repositories.Model.Blog;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BlogService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateBlog(CreateBlogModel model)
        {
            Blog blog=mapper.Map<Blog>(model);
            await unitOfWork.GetRepository<Blog>().InsertAsync(blog);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteBlog(string id)
        {
            var delBlog = await unitOfWork.GetRepository<Blog>().Entities
                .FirstOrDefaultAsync(b => b.BlogID.ToString() == id);
            unitOfWork.BeginTransaction();
            await unitOfWork.GetRepository<Blog>().DeleteAsync(delBlog);
            unitOfWork.CommitTransaction();
            await unitOfWork.SaveAsync();
        }

        public async Task<IList<BlogServiceModel>> GetBLogAsync()
        {
            var blogList= await unitOfWork.GetRepository<Blog>().Entities.ToListAsync();
            return mapper.Map<IList<BlogServiceModel>>(blogList);
        }

        public Task<BlogServiceModel> GetBlogAsyncById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBlog(UpdateBlogModel model, string id)
        {
            throw new NotImplementedException();
        }
    }
}
