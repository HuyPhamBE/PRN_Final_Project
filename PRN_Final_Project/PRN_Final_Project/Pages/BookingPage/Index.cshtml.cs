using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;
using Services.Interface;

namespace PRN_Final_Project.Pages.BookingPage
{
    public class IndexModel : PageModel
    {        
        private readonly IBookingService bookingService;

        public IndexModel(IBookingService bookingService)
        {            
            this.bookingService = bookingService;
        }

        public IList<Booking> Booking { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Booking =await bookingService.GetAll();
        }
    }
}
