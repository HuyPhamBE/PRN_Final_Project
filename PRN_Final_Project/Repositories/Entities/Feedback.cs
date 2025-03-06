using Repositories.Entities.Base;
using System.ComponentModel.DataAnnotations;
namespace Repositories.Entities
{
    public class Feedback : BaseEntity
    {
        [Key]
        public Guid FeedbackID { get; set; }
        public string content {  get; set; }
        public Guid accountID { get; set; }
        public Guid serviceID { get; set; }
        public virtual Account Account { get; set; }
        public virtual Service Service { get; set; }
    }
}
