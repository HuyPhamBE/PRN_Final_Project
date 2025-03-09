using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;

namespace PRN_Final_Project.Pages.CustomerManage
{
    public class IndexModel : PageModel
    {
        private readonly Repositories.DB.ApplicationDbContext _context;

        public IndexModel(Repositories.DB.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Customer = await _context.Customers
                .Include(c => c.Account).ToListAsync();
        }
    }
}
