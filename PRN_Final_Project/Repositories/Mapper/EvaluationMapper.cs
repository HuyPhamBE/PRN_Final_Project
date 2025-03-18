using AutoMapper;
using Repositories.Entities;
using Repositories.Model.Blog;
using Repositories.Model.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Mapper
{
    public class EvaluationMapper : Profile
    {
        public EvaluationMapper()
        {
            CreateMap<CreateEvaluationModel, Evaluation>().ReverseMap();
            CreateMap<UpdateEvaluationModel, Evaluation>().ReverseMap();
            CreateMap<EvaluationServiceModel, Evaluation>().ReverseMap();
        }
    }
}
