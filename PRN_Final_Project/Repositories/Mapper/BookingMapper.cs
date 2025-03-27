using AutoMapper;
using Repositories.Entities;
using Repositories.Model.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Mapper
{
    public class BookingMapper : Profile
    {
        public BookingMapper()
        {
            CreateMap<CreateBookingModel, Booking>().ReverseMap();
            CreateMap<UpdateBookingModel, Booking>().ReverseMap();
            CreateMap<BookingServiceModel, Booking>().ReverseMap();
        }
    }
}
