using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Model.Booking
{
    public class CreateBookingModel
    {
        public DateTime appointmentDay { get; set; }
        public string status {  get; set; }
        public decimal total { get; set; }        
        public Guid serviceID { get; set; }
        public Guid slotID { get; set; }
        public Guid? theraID { get; set; }
        public Guid cusID { get; set; }
    }
}
