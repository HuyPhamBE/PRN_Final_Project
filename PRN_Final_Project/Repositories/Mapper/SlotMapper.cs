using AutoMapper;
using Repositories.Entities;
using Repositories.Model.Slot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Mapper
{
    public class SlotMapper : Profile
    {
        public SlotMapper()
        {
            CreateMap<CreateSlotModel, Slot>().ReverseMap();
            CreateMap<UpdateSlotModel, Slot>().ReverseMap();
            CreateMap<SlotServiceModel, Slot>().ReverseMap();
        }
    }
}
