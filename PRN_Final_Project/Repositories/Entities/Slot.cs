using Repositories.Entities.Base;
using System.ComponentModel.DataAnnotations;
namespace Repositories.Entities
{
    public class Slot : BaseEntity
    {
        [Key]
        public Guid SlotID { get; set; }
        public TimeSpan startTime {  get; set; }
        public TimeSpan endTime {  get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
