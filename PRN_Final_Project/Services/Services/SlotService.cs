using Repositories.Model.Slot;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class SlotService : ISlotService
    {
        public Task CreateSlot(CreateSlotModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSlot(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<SlotServiceModel>> GetSlotAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SlotServiceModel> GetSlotAsyncById(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSlot(UpdateSlotModel model, string id)
        {
            throw new NotImplementedException();
        }
    }
}
