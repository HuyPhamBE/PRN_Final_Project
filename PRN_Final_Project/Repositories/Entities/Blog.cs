using Repositories.Entities.Base;
namespace Repositories.Entities
{
    public class Blog : BaseEntity
    {
        public Guid BlogID { get; set; }
        public string title {  get; set; }
        public string content {  get; set; }
        public string imageUrl {  get; set; }
        public Guid accountID { get; set; }
        public virtual Account Account { get; set; }
    }
}
