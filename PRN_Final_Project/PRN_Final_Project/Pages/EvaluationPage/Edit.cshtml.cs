using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.Evaluation;
using Services.Interface;

namespace PRN_Final_Project.Pages.EvaluationPage
{
    public class EditModel : PageModel
    {
        private readonly IEvaluationService evaluationService;

        public EditModel(IEvaluationService evaluationService)
        {
            this.evaluationService = evaluationService;
        }

        [BindProperty]
        public UpdateEvaluationModel Evaluation { get; set; } = default!;

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await evaluationService.UpdateEvaluation(Evaluation, Evaluation.evaID.ToString());
             return RedirectToPage("./Index");
        }
    }
}
