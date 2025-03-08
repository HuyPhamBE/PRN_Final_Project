using Repositories.Entities.Base;
using System.ComponentModel.DataAnnotations;
namespace Repositories.Entities
{
    public class Service : BaseEntity
    {
        [Key]
        public Guid ServiceID { get; set; }
        public string serviceName {  get; set; }
        public string description {  get; set; }
        public int minRage {  get; set; }
        public int maxRage { get; set; }
        public string status {  get; set; }
        public decimal price {  get; set; }
        public Guid ServiceTypeID { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
