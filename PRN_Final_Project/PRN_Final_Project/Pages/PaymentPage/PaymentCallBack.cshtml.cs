using Azure;
using Entities.IUOW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.Booking;
using Services.Interface;
using Services.Services;

namespace PRN_Final_Project.Pages.PaymentPage
{
    public class PaymentCallBackModel : PageModel
    {
        private readonly IVnPayService vnPayService;
        private readonly IPaymentService paymentService;
        private readonly IUnitOfWork unitOfWork;

        public PaymentCallBackModel(IVnPayService vnPayService,
            IPaymentService paymentService,
            IUnitOfWork unitOfWork)
        {
            this.vnPayService = vnPayService;
            this.paymentService = paymentService;
            this.unitOfWork = unitOfWork;
        }
        public CreateBookingModel BookingModel { get; set; }
        public decimal deposit { get; set; }
        [BindProperty]
        public Payment PaymentDetails { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Info { get; set; }
        [BindProperty(SupportsGet = true)]
        public string vnp_TransactionNo { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
            Info = Request.Query["vnp_OrderInfo"];
            vnp_TransactionNo = Request.Query["vnp_TransactionNo"];
            TempData["TransNo"] = vnp_TransactionNo;
            if (Request.Cookies.TryGetValue("BookingData", out var bookingDataStr) &&
            !string.IsNullOrEmpty(bookingDataStr))
            {
                BookingModel = JsonConvert.DeserializeObject<CreateBookingModel>(bookingDataStr);
            }
            else
            {
                ErrorMessage = "Booking data is missing.";
            }
            deposit = BookingModel.total * 0.5m;
        }
        public async Task<IActionResult> OnGetPaymentCallbackVnpay()
        {
            var response = vnPayService.PaymentExecute(Request.Query);

            if (response == null || !response.Success)
            {
                ErrorMessage = "Payment failed or invalid response.";
                return Page();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostSavePayment()
        {
            vnp_TransactionNo = TempData.Peek("TransNo")?.ToString();
            if (Request.Cookies.TryGetValue("BookingData", out var bookingDataStr) &&
           !string.IsNullOrEmpty(bookingDataStr))
            {
                BookingModel = JsonConvert.DeserializeObject<CreateBookingModel>(bookingDataStr);
            }
            else
            {
                ErrorMessage = "Booking data is missing.";
            }
            deposit = BookingModel.total * 0.5m;
            var cus=await unitOfWork.GetRepository<Customer>().Entities.FirstOrDefaultAsync(x => x.accountID == Guid.Parse(HttpContext.Session.GetString("UserId")));

            PaymentDetails = new Payment
            {
                paymentID = Guid.NewGuid(),
                amount = deposit,
                orderInfo = HttpContext.Session.GetString("UserName") +" thanh toan "+deposit,
                status = "Success",
                bankCode = "NCB",
                transactionNo= vnp_TransactionNo,
                paymentDay = DateTime.Now,
                cusID = cus.cusID,
                bookingID = BookingModel.BookingID
            };
            await paymentService.AddPayment(PaymentDetails);
            return RedirectToPage("/BookingPage/BookingHistory");
        }
    }
}
