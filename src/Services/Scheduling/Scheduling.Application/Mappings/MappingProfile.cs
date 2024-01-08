using AutoMapper;
using Scheduling.Application.Features.Schedules.Commands.ReserveAppointment;
using Scheduling.Application.Features.Schedules.Queries.GetAppointmentsListQuery;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppointmentSlot, AppointmentSlotVM>().ReverseMap();
            CreateMap<ReserveAppointmentCommand, AppointmentSlot>();
        }
    }
}
