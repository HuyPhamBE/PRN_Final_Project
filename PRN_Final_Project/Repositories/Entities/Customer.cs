using Repositories.Entities.Base;
namespace Repositories.Entities
{
    public class Customer : BaseEntity
    {
        public Guid cusID { get; set; }
        public string fullName {  get; set; }
        public string phone {  get; set; }
        public string address {  get; set; }
        public DateTime dob { get; set; }
        public bool gender {  get; set; }
        public string imageUrl {  get; set; }
        public Guid accountID { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
