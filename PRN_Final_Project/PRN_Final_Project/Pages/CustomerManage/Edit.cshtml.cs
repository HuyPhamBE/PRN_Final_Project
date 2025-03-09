using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.Customer;
using Services.Interface;

namespace PRN_Final_Project.Pages.CustomerManage
{
    public class EditModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public EditModel(ICustomerService customerService)
        { 
            this._customerService = customerService;
        }
        public string id {  get; set; }
        [BindProperty]
        public UpdateCustomerModel Customer { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           _customerService.UpdateCustomer(Customer,Customer.cusId.ToString());

            return RedirectToPage("./Index");
        }
    }
}
