using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Model.Blog;
namespace Services.Interface
{
    public interface IBlogService
    {
        Task<BlogServiceModel> GetBlogAsyncById(string id);
        Task<IList<BlogServiceModel>> GetBLogAsync();
        Task CreateBlog(CreateBlogModel model);
        Task DeleteBlog(string id);
        Task UpdateBlog(UpdateBlogModel model,string id);
    }
}
