using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.Slot;
using Services.Interface;

namespace PRN_Final_Project.Pages.SlotPage
{
    public class IndexModel : PageModel
    {
        private readonly ISlotService slotService;

        public IndexModel(ISlotService slotService)
        {
            this.slotService = slotService;
        }

        public IList<SlotServiceModel> Slot { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Slot = await slotService.GetSlotAsync();
        }
    }
}
