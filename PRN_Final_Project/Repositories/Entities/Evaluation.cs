using Repositories.Entities.Base;
using System.ComponentModel.DataAnnotations;
namespace Repositories.Entities
{
    public class Evaluation : BaseEntity
    {
        [Key]
        public Guid evaID { get; set; }
        public int point {  get; set; }
        public string status {  get; set; }
        public Guid accountID { get; set; }
        public virtual Account Accounts { get; set; }
    }
}
