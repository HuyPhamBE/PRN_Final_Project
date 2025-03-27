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
    public class DeleteModel : PageModel
    {
        private readonly ISlotService slotService;

        public DeleteModel(ISlotService slotService)
        {
            this.slotService = slotService;
        }

        [BindProperty]
        public SlotServiceModel Slot { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slot = await slotService.GetSlotAsyncById(id);

            if (slot == null)
            {
                return NotFound();
            }
            else
            {
                Slot = slot;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await slotService.DeleteSlot(id);

            return RedirectToPage("./Index");
        }
    }
}
