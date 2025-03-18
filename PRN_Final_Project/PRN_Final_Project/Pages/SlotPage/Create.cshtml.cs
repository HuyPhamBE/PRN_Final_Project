using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.Slot;
using Services.Interface;

namespace PRN_Final_Project.Pages.SlotPage
{
    public class CreateModel : PageModel
    {
        private readonly ISlotService slotService;

        public CreateModel(ISlotService slotService)
        {
            this.slotService = slotService;
        }
            

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateSlotModel Slot { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await slotService.CreateSlot(Slot);
            return RedirectToPage("./Index");
        }
    }
}
