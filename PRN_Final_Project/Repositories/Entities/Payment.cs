using Repositories.Entities.Base;
namespace Repositories.Entities
{
    public class Payment
    {
        public Guid paymentID { get; set; }
        public decimal amount { get; set; }
        public string orderInfo { get; set; }
        public int responseCode { get; set; }
        public string transactionNo { get; set; }
        public string status { get; set; }
        public string bankCode { get; set; }
        public DateTime paymentDay { get; set; }
        public Guid cusID { get; set; }
        public Guid bookingID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
