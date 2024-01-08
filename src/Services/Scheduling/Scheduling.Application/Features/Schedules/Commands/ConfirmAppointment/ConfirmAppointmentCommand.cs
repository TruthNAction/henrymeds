using MediatR;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Features.Schedules.Commands.ConfirmAppointment
{
    public class ConfirmAppointmentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public SlotStatus SlotStatus { get; set; }
    }
}
