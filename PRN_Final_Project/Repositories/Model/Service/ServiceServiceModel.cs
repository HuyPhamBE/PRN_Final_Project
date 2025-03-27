using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Model.Service
{
    public class ServiceServiceModel
    {
        public Guid ServiceID { get; set; }
        public string serviceName { get; set; }
        public string description { get; set; }
        public int minRange { get; set; }
        public int maxRange { get; set; }
        public string status { get; set; }
        public decimal price { get; set; }
        public Guid ServiceTypeID { get; set; }
    }
}
