using AutoMapper;
using Entities.IUOW;
using Repositories.Entities;
using Repositories.Model.Payment;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PaymentService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddPayment(Payment paymentModel)
        {
            await unitOfWork.GetRepository<Payment>().InsertAsync(paymentModel);
            await unitOfWork.SaveAsync();
        }
    }
}
