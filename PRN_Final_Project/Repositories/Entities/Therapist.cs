using Repositories.Entities.Base;
using System.ComponentModel.DataAnnotations;
namespace Repositories.Entities
{
    public class Therapist : BaseEntity
    {
        [Key]
        public Guid theraID { get; set; }
        public string fullName {  get; set; }
        public bool gender {  get; set; }
        public string major {  get; set; }
        public string imageUrl {  get; set; }
        public int exp {  get; set; }
        public string status {  get; set; }
        public Guid accountID { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
