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

namespace PRN_Final_Project.Pages.TherapyResultPage
{
    public class DetailsModel : PageModel
    {
        private readonly ITherapistResultService _therapistResultService;
        private readonly IBookingService _bookingService;

        public DetailsModel(ITherapistResultService therapistResultService, IBookingService bookingService)
        {
            _therapistResultService = therapistResultService;
            _bookingService = bookingService;
        }

        public TherapyResult TherapyResult { get; set; } = default!;
        public Booking Booking { get; set; } = default!;
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TherapyResult = await _therapistResultService.GetTherapyResultById(id.Value);
            if (TherapyResult == null)
            {
                return NotFound();
            }

            Booking = await _bookingService.GetById(TherapyResult.bookingID);
            if (Booking == null)
            {
                return NotFound();
            }

            Customer = Booking.Customer ?? new Customer(); 

            return Page();
        }
    }
}
