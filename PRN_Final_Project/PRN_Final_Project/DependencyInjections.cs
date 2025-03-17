﻿using Entities.IUOW;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Services.Interface;
using Services.Services;
namespace PRN_Assignment
{
    public static class DependencyInjections
    {
        public static void AddDI(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddDatabase(configuration);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<ITherapistService, TherapistService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ICustomerService, CustomerService>();
        }
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
