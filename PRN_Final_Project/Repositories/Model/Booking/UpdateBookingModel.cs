using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Model.Booking
{
    public class UpdateBookingModel
    {
        public DateTime appointmentDay { get; set; }
        public Guid slotID { get; set; }
        public string status { get; set; }
    }
}
