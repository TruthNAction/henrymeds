using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Application.Features.Schedules.Commands.SubmitSchedule;
using Scheduling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling.Application.Exceptions;

namespace Scheduling.Application.Features.Schedules.Commands.ReserveAppointment
{
    public class ReserveAppointmentCommandHandler : IRequestHandler<ReserveAppointmentCommand, Guid>
    {
        private readonly IAppointmentSlotRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ReserveAppointmentCommandHandler> _logger;

        public ReserveAppointmentCommandHandler(IAppointmentSlotRepository appointmentRepository, IMapper mapper, ILogger<ReserveAppointmentCommandHandler> logger)
        {
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Guid> Handle(ReserveAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointmentSlot = await _appointmentRepository.GetByIdAsync(request.Id);
            if (appointmentSlot == null)
                throw new NotFoundException(nameof(AppointmentSlot), request.Id);

            if ((appointmentSlot.SlotDateTime - DateTime.Now).TotalHours < 24)
                throw new ApplicationException("Appointment can't be made in less than 24 hours.");

            if (appointmentSlot is { PatientId: not null, Status: SlotStatus.Reserved } &&
                (appointmentSlot.StatusDate - DateTime.Now).TotalMinutes < 30)
                throw new ApplicationException("Appointment is already reserved.");

            appointmentSlot.Status = SlotStatus.Reserved;
            appointmentSlot.StatusDate = DateTime.Now;

            _mapper.Map(request, appointmentSlot);

            return appointmentSlot.Id;
        }
    }
}
