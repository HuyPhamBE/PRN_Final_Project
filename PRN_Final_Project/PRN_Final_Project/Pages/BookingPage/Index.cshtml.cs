using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;

namespace PRN_Final_Project.Pages.BookingPage
{
    public class IndexModel : PageModel
    {
        private readonly Repositories.DB.ApplicationDbContext _context;

        public IndexModel(Repositories.DB.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Service)
                .Include(b => b.Slot)
                .Include(b => b.Therapist).ToListAsync();
        }
    }
}
