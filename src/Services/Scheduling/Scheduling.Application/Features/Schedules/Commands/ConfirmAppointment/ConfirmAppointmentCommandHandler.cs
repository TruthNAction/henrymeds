using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Application.Exceptions;
using Scheduling.Application.Features.Schedules.Commands.ReserveAppointment;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Features.Schedules.Commands.ConfirmAppointment
{
    public class ConfirmAppointmentCommandHandler : IRequestHandler<ConfirmAppointmentCommand, Guid>
    {
        private readonly IAppointmentSlotRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ConfirmAppointmentCommandHandler> _logger;

        public ConfirmAppointmentCommandHandler(IAppointmentSlotRepository appointmentRepository, IMapper mapper, ILogger<ConfirmAppointmentCommandHandler> logger)
        {
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Guid> Handle(ConfirmAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointmentSlot = await _appointmentRepository.GetByIdAsync(request.Id);
            if (appointmentSlot == null)
                throw new NotFoundException(nameof(AppointmentSlot), request.Id);

            if (appointmentSlot.PatientId != request.PatientId)
                throw new ApplicationException("Patient does not have this slot reserved.");

            appointmentSlot.Status = SlotStatus.Reserved;
            appointmentSlot.StatusDate = DateTime.Now;

            _mapper.Map(request, appointmentSlot);

            return appointmentSlot.Id;
        }
    }
}
