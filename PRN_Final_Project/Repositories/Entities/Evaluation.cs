using Repositories.Entities.Base;
namespace Repositories.Entities
{
    public class Evaluation : BaseEntity
    {
        public Guid evaID { get; set; }
        public int point {  get; set; }
        public string status {  get; set; }
        public Guid accountID { get; set; }
        public virtual Account Accounts { get; set; }
    }
}
