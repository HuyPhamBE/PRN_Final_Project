using Repositories.Entities.Base;
namespace Repositories.Entities
{
    public class Booking : BaseEntity
    {
        public Guid BookingID { get; set; }
        public DateTime appointmentDay {  get; set; }
        public string status {  get; set; }
        public decimal total {  get; set; }
        public decimal deposit {  get; set; }
        public Guid serviceID { get; set; }
        public Guid slotID { get; set; }
        public Guid theraID { get; set; }
        public Guid cusID { get; set; }
        public virtual Service Service { get; set; }
        public virtual Slot Slot { get; set; }
        public virtual Therapist Therapist { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<TherapyResult> TherapyResults { get; set; }
    }
}
