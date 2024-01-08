using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling.Domain.Common;

namespace Scheduling.Domain.Entities
{
    public class AppointmentSlot : EntityBase
    {
        public AppointmentSlot()
        {
            
        }

        public AppointmentSlot(Guid providerId, SlotStatus status, DateTime slotDateTime)
        {
            ProviderId = providerId;
            Status = status;
            SlotDateTime = slotDateTime;
        }

        public Guid ProviderId { get; set; }
        public Guid? PatientId { get; set; }
        public SlotStatus Status { get; set; }
        public DateTime StatusDate { get; set; }
        public DateTime SlotDateTime { get; set; }

    }
}
