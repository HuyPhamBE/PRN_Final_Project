using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories.DB;
using Repositories.Entities;
using Repositories.Model.Slot;
using Services.Interface;

namespace PRN_Final_Project.Pages.SlotPage
{
    public class EditModel : PageModel
    {
        private readonly ISlotService slotService;
        private readonly IMapper mapper;

        public EditModel(ISlotService slotService,IMapper mapper)
        {
            this.slotService = slotService;
            this.mapper = mapper;
        }

        [BindProperty]
        public UpdateSlotModel Slot { get; set; } = default!;


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await slotService.UpdateSlot(Slot, Slot.SlotID.ToString());
            return RedirectToPage("./Index");
        }
    }
}
