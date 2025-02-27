using Repositories.Entities.Base;
namespace Repositories.Entities
{
    public class Rating : BaseEntity
    {
        public Guid rateID { get; set; }
        public Byte rates {  get; set; }
        public Guid theraID { get; set; }
        public virtual Therapist Therapist { get; set; }
    }
}
