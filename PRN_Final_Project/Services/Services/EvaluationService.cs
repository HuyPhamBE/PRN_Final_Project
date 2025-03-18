using Repositories.Model.Evaluation;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Entities;
using Entities.IUOW;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
namespace Services.Services
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public EvaluationService(IUnitOfWork unitOfWork,IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateEvaluation(CreateEvaluationModel model)
        {
            Evaluation eva= mapper.Map<Evaluation>(model);
            eva.accountID = Guid.TryParse(httpContextAccessor.HttpContext.Request.Cookies["AccountID"], out Guid accountID)
                ? accountID
                : Guid.Empty;
            await unitOfWork.GetRepository<Evaluation>().InsertAsync(eva);
            await unitOfWork.SaveAsync();
        }

        public async Task<IList<EvaluationServiceModel>> GetEvaluationAsync()
        {
            var evaluations = await unitOfWork.GetRepository<Evaluation>().GetAllAsync();
            return mapper.Map<IList<EvaluationServiceModel>>(evaluations);
        }

        public async Task<EvaluationServiceModel> GetEvaluationAsyncById(string id)
        {
            var eva = await unitOfWork.GetRepository<Evaluation>().GetByIdAsync(id);
            return mapper.Map<EvaluationServiceModel>(eva);
        }

        public async Task UpdateEvaluation(UpdateEvaluationModel model, string id)
        {             
            var eva = mapper.Map<Evaluation>(model);
            await unitOfWork.GetRepository<Evaluation>().UpdateAsync(eva);
            await unitOfWork.SaveAsync();
        }
    }
}
