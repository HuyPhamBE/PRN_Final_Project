using Repositories.Entities.Base;
using System.ComponentModel.DataAnnotations;
namespace Repositories.Entities
{
    public class Account : BaseEntity
    {
        [Key]
        public Guid accountID { get; set; }
        public string userName {  get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string role {  get; set; }
        public string status {  get; set; }       

        public virtual ICollection<Therapist> Therapists { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
