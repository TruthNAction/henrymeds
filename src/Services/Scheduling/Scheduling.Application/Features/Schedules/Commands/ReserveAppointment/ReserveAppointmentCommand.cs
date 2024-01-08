using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Scheduling.Application.Features.Schedules.Commands.ReserveAppointment
{
    public class ReserveAppointmentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
    }
}
