using Entities.IUOW;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using PRN_Assignment;
using Repositories.DB;
using Repositories.Entities;
using Services.Interface;
using Services.Services;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDI(builder.Configuration);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
         .AddCookie(options =>
         {
             options.LoginPath = "/";
             options.AccessDeniedPath = "/Error";
         });

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ITherapistService, TherapistService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper((Assembly[])AppDomain.CurrentDomain.GetAssemblies());
//// Add session
//builder.Services.AddSession();  
builder.Services.AddScoped<ITherapistService, TherapistService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ITherapistResultService, TherapistResultService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//// Use session
//app.UseSession();

app.UseAuthentication();    
app.UseAuthorization();

app.MapRazorPages();

app.Run();
