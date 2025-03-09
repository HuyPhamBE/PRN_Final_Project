using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.Customer;
using Services.Interface;

namespace PRN_Final_Project.Pages.CustomerManage
{
    public class DetailsModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public DetailsModel(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        public CustomerServiceModel Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _customerService.GetCustomerAsyncById(Customer.cusID.ToString());
            return Page();
        }
    }
}
