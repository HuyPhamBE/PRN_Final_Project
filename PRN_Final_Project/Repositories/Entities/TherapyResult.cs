using Repositories.Entities.Base;
namespace Repositories.Entities
{
    public class TherapyResult : BaseEntity
    {
        public Guid theraResultID { get; set; }
        public string status {  get; set; }
        public string content {  get; set; }
        public Guid bookingID { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
