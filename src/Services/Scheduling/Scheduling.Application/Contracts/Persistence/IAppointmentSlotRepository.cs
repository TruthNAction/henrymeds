using Scheduling.Domain.Entities;

namespace Scheduling.Application.Contracts.Persistence
{
    public interface IAppointmentSlotRepository : IAsyncRepository<AppointmentSlot>
    {
        Task<IEnumerable<AppointmentSlot>> GetAvailableSlotsByDateRange(DateTime dateFrom, DateTime dateTo);
    }
}
