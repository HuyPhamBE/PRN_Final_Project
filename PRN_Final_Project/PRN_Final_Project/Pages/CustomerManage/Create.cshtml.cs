using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.Customer;
using Services.Interface;

namespace PRN_Final_Project.Pages.CustomerManage
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CreateModel(ICustomerService customerService)
        {            
            this._customerService = customerService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateCustomerModel Customer { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _customerService.CreateCustomer(Customer);

            return RedirectToPage("./Index");
        }
    }
}
