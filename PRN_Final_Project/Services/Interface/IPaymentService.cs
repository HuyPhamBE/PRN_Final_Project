using Repositories.Entities;
using Repositories.Model.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IPaymentService
    {
        Task AddPayment(Payment paymentModel);
    }
}
