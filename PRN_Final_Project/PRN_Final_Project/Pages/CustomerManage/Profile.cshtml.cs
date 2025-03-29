using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.Customer;
using Services.Interface;
using Services.Services;

namespace PRN_Final_Project.Pages.CustomerManage
{
    public class ProfileModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public ProfileModel(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        public CustomerServiceModel Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var uID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userIdStr = HttpContext.Session.GetString("UserId");
            Customer cus = await _customerService.GetCustomerByUserId(Guid.Parse(uID));
            Customer = await _customerService.GetCustomerAsyncById(cus.cusID.ToString());
            return Page();
        }
    }
}
