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

namespace PRN_Final_Project.Pages.Staff
{
    public class DetailsModel : PageModel
    {
        private readonly IBookingService _bookingService;
        
        public DetailsModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public Booking Booking { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(Guid? id)
        {

            var booking = await _bookingService.GetBookingByID(id.GetValueOrDefault());
            if (booking == null)
            {
                return NotFound();
            }
            else
            {
                Booking = booking;
            }
            return Page();
        }
    }
}
