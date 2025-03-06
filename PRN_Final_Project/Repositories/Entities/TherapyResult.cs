using Repositories.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Repositories.Entities
{
    public class TherapyResult : BaseEntity
    {
        [Key]
        public Guid theraResultID { get; set; }
        public string status {  get; set; }
        public string content {  get; set; }
        public Guid bookingID { get; set; }
        [ForeignKey(nameof(bookingID))]
        public virtual Booking Booking { get; set; }
    }
}
