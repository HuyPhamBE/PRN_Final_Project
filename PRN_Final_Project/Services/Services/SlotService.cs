using AutoMapper;
using Entities.IUOW;
using Repositories.Entities;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SlotService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task CreateSlot(CreateSlotModel model)
        {
            Slot slot = mapper.Map<Slot>(model);
            await unitOfWork.GetRepository<Slot>().InsertAsync(slot);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteSlot(string id)
        {
            await unitOfWork.GetRepository<Slot>().DeleteAsync(id);
            await unitOfWork.SaveAsync();
        }

        public async Task<IList<SlotServiceModel>> GetSlotAsync()
        {
            var Slots = await unitOfWork.GetRepository<Slot>().GetAllAsync();
            return mapper.Map<IList<SlotServiceModel>>(Slots);
        }

        public async Task<SlotServiceModel> GetSlotAsyncById(string id)
        {
            var slot = await unitOfWork.GetRepository<Slot>().GetByIdAsync(id);
            return mapper.Map<SlotServiceModel>(slot);
        }

        public async Task UpdateSlot(UpdateSlotModel model, string id)
        {
            var slot = mapper.Map<Slot>(model);
            await unitOfWork.GetRepository<Slot>().UpdateAsync(slot);
            await unitOfWork.SaveAsync();
        }
    }
}
