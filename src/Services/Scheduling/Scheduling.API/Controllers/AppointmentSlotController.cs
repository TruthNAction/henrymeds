using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Scheduling.Application.Features.Schedules.Commands.ConfirmAppointment;
using Scheduling.Application.Features.Schedules.Queries.GetAppointmentsListQuery;
using Scheduling.Application.Features.Schedules.Commands.SubmitSchedule;
using Scheduling.Application.Features.Schedules.Commands.ReserveAppointment;

namespace Scheduling.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentSlotController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentSlotController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet(Name = "GetAvailableSlots")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentSlotVM>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<AppointmentSlotVM>>> GetAvailableSlots([FromQuery] DateTime dateFrom,
            DateTime dateTo)
        {
            var query = new GetAppointmentListQuery(dateFrom, dateTo);
            var slots = await _mediator.Send(query);
            return Ok(slots);
        }

        [HttpPost(Name = "SubmitSchedule")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> UpdateSchedule([FromBody] SubmitScheduleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut(Name = "ReserveAppointment")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> UpdateSchedule([FromBody] ReserveAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut(Name = "ConfirmAppointment")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> ConfirmAppointment([FromBody] ConfirmAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}