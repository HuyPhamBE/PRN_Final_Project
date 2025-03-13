using Repositories.Model.Slot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ISlotService
    {
        Task<SlotServiceModel> GetSlotAsyncById(string id);
        Task<IList<SlotServiceModel>> GetSlotAsync();
        Task CreateSlot(CreateSlotModel model);
        Task DeleteSlot(string id);
        Task UpdateSlot(UpdateSlotModel model, string id);
    }
}
