using Repositories.Entities.Base;
namespace Repositories.Entities
{
    public class Slot : BaseEntity
    {
        public Guid SlotID { get; set; }
        public TimeSpan startTime {  get; set; }
        public TimeSpan endTime {  get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
