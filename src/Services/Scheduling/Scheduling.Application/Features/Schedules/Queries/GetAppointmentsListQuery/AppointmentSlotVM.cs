using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Features.Schedules.Queries.GetAppointmentsListQuery
{
    public class AppointmentSlotVM
    {
        public Guid ProviderId { get; set; }
        public Guid PatientId { get; set; }
        public int Status { get; set; }
        public DateTime SlotDateTime { get; set; }
    }
}
