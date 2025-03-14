using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;
using Services.Interface;

namespace PRN_Final_Project.Pages.TherapyResultPage
{
    public class IndexModel : PageModel
    {
        private readonly ITherapistResultService _therapistResultService;

        public IndexModel(ITherapistResultService therapistResultService)
        {
            _therapistResultService = therapistResultService;
        }

        public IList<TherapyResult> TherapyResult { get;set; } = default!;

        public async Task OnGetAsync()
        {
            TherapyResult = await _therapistResultService.GetAll();
        }
    }
}
