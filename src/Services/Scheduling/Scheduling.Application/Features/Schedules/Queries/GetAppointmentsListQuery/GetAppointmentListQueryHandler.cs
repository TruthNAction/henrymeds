using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Features.Schedules.Queries.GetAppointmentsListQuery
{
    public class GetAppointmentListQueryHandler : IRequestHandler<GetAppointmentListQuery, IEnumerable<AppointmentSlotVM>>
    {
        private readonly IAppointmentSlotRepository _slotRespository;
        private readonly IMapper _mapper;

        public GetAppointmentListQueryHandler(IAppointmentSlotRepository slotRespository, IMapper mapper)
        {
            _slotRespository = slotRespository ?? throw new ArgumentNullException(nameof(slotRespository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<AppointmentSlotVM>> Handle(GetAppointmentListQuery request, CancellationToken token)
        {
            var slots = await _slotRespository.GetAvailableSlotsByDateRange(request.DateFrom, request.DateTo);

            var expiredSlots = slots
                .Where(s => s.Status == SlotStatus.Reserved && (s.StatusDate - DateTime.Now).TotalMinutes > 30);
            
            foreach (var slot in expiredSlots) slot.Status = SlotStatus.Available;

            return _mapper.Map<IEnumerable<AppointmentSlotVM>>(slots);
        }
    }
}
