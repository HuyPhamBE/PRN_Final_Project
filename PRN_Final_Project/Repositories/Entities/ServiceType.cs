using Repositories.Entities.Base;
namespace Repositories.Entities
{
    public class ServiceType : BaseEntity
    {
        public Guid ServiceTypeID { get; set; }
        public string serviceName {  get; set; }
        public int minRage { get; set; }
        public int maxRage { get; set; }
        public string status { get; set; }
        public decimal price { get; set; }
    }
}
