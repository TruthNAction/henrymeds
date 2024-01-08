using MediatR;

namespace Scheduling.Application.Features.Schedules.Commands.SubmitSchedule
{
    public class SubmitScheduleCommand : IRequest<Unit>
    {
        public Guid ProviderId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
