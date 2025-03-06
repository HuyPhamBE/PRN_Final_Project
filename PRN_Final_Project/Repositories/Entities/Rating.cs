using Repositories.Entities.Base;
using System.ComponentModel.DataAnnotations;
namespace Repositories.Entities
{
    public class Rating : BaseEntity
    {
        [Key]
        public Guid rateID { get; set; }
        public Byte rates {  get; set; }
        public Guid theraID { get; set; }
        public virtual Therapist Therapist { get; set; }
    }
}
