using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IBookingService
    {
        Task<IList<Booking>> GetByTherapistId(Guid therapistId);

        Task<Booking?> GetById(Guid id);
    }
}
