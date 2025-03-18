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
    public class DetailsModel : PageModel
    {
        private readonly IEvaluationService evaluationService;

        public DetailsModel(IEvaluationService evaluationService)
        {
            this.evaluationService = evaluationService;
        }

        public EvaluationServiceModel Evaluation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluation = await evaluationService.GetEvaluationAsyncById(id.ToString());
            if (evaluation == null)
            {
                return NotFound();
            }
            else
            {
                Evaluation = evaluation;
            }
            return Page();
        }
    }
}
