using Repositories.Entities.Base;
namespace Repositories.Entities
{
    public class Feedback : BaseEntity
    {
        public Guid FeedbackID { get; set; }
        public string content {  get; set; }
        public Guid accountID { get; set; }
        public Guid serviceID { get; set; }
        public virtual Account Account { get; set; }
        public virtual Service Service { get; set; }
    }
}
