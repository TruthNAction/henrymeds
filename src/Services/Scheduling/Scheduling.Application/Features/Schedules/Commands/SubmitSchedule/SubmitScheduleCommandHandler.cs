using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Domain.Entities;
using Scheduling.Application.Exceptions;

namespace Scheduling.Application.Features.Schedules.Commands.SubmitSchedule
{
    public class SubmitScheduleCommandHandler : IRequestHandler<SubmitScheduleCommand, Unit>
    {
        private readonly IAppointmentSlotRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SubmitScheduleCommandHandler> _logger;

        public SubmitScheduleCommandHandler(IAppointmentSlotRepository appointmentRepository, IMapper mapper, ILogger<SubmitScheduleCommandHandler> logger)
        {
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(SubmitScheduleCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<AppointmentSlot> slots = Enumerable
                .Range(0, (int)((request.StartDateTime - request.EndDateTime).TotalMinutes / 15) + 1)
                .Select(i => new AppointmentSlot(request.ProviderId, SlotStatus.Available,
                    request.StartDateTime.AddMinutes(i * 15)));


            await _appointmentRepository.AddManyAsync(slots);

            _logger.LogInformation($"Appointments are successfully submitted.");

            return Unit.Value;
        }
    }
}
