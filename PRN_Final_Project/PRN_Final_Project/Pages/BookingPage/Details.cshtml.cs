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
    public class DetailsModel : PageModel
    {
        private readonly IBookingService bookingService;

        public DetailsModel(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        public Booking Booking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Booking = await bookingService.GetBookingByID(id);
            return Page();
        }
    }
}
