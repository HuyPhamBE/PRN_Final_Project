using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.Customer;
using Services.Interface;
using Services.Services;

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
        public UpdateCustomerModel Customer { get; set; }
        public string acc { get; set; }
        public async Task OnGetAsync()
        {
            acc = HttpContext.Session.GetString("UserId");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var accID = HttpContext.Session.GetString("UserId");
            var cus = await _customerService.GetCustomerByUserId(Guid.Parse(accID));
            Customer.accountID = Guid.Parse(accID);
            Customer.cusId = cus.cusID;
            await _customerService.UpdateCustomer(Customer, cus.cusID);
            return RedirectToPage("/CustomerManage/Profile");
        }
    }
}
