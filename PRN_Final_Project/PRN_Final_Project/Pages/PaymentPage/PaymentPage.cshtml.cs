using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Repositories.Entities;
using Repositories.Model.Booking;
using Repositories.Model.Payment;
using Services.Interface;
using Services.Services;

namespace PRN_Final_Project.Pages.PaymentPage
{
    public class PaymentPageModel : PageModel
    {
        private readonly IServiceService serviceService;
        private readonly IVnPayService vnPayService;

        public PaymentPageModel(IServiceService serviceService,IVnPayService vnPayService)
        {
            this.serviceService = serviceService;
            this.vnPayService = vnPayService;
        }

        public CreateBookingModel BookingModel { get; set; }
        public string description {  get; set; }
        public decimal deposit { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (TempData["BookingData"] == null)
            {
                // Handle the missing booking data appropriately.
                // For example, redirect back to the booking creation page or display an error.
                return RedirectToPage("/BookingPage/Create");
            }

            BookingModel = JsonConvert.DeserializeObject<CreateBookingModel>((string)TempData["BookingData"]);
            TempData.Keep("BookingData");
            description = (await serviceService.GetServiceAsyncById(BookingModel.serviceID)).description;
            deposit = BookingModel.total * 0.5m;
            return Page();
        }
        
        public IActionResult OnPostCreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            var url = vnPayService.CreatePaymentUrl(model, HttpContext);
            BookingModel = JsonConvert.DeserializeObject<CreateBookingModel>((string)TempData["BookingData"]);
            TempData.Keep("BookingData");
            var bookingJson = JsonConvert.SerializeObject(BookingModel);
            // Store the data in a secure, HttpOnly cookie with a short expiration time
            Response.Cookies.Append("BookingData", bookingJson);
            return Redirect(url);
        }
    }
}
