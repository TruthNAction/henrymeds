using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Features.Schedules.Queries.GetAppointmentsListQuery
{
    public class GetAppointmentListQuery : IRequest<IEnumerable<AppointmentSlotVM>>
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }


        public GetAppointmentListQuery(DateTime dateFrom, DateTime dateTo)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
}
