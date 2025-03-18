using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Model.Slot
{
    public class CreateSlotModel
    {
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
    }
}
