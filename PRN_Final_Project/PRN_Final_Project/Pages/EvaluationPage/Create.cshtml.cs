using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.Evaluation;
using Services.Interface;

namespace PRN_Final_Project.Pages.EvaluationPage
{
    public class CreateModel : PageModel
    {
        private readonly IEvaluationService evaluationService;

        public CreateModel(IEvaluationService evaluationService)
        {
            this.evaluationService = evaluationService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateEvaluationModel Evaluation { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           await evaluationService.CreateEvaluation(Evaluation);

            return RedirectToPage("./Index");
        }
    }
}
