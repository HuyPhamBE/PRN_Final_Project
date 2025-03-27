using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.Evaluation;
using Services.Interface;

namespace PRN_Final_Project.Pages.EvaluationPage
{
    public class IndexModel : PageModel
    {
        private readonly IEvaluationService evaluationService;

        public IndexModel(IEvaluationService evaluationService)
        {
            this.evaluationService = evaluationService;
        }

        public IList<EvaluationServiceModel> Evaluation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Evaluation = await evaluationService.GetEvaluationsAsync();
        }
    }
}
