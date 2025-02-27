using Microsoft.EntityFrameworkCore;
using Repositories.DB;
namespace PRN_Assignment
{
    public static class DependencyInjections
    {
        public static void AddDI(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddDatabase(configuration);
        }
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
