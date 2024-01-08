using Microsoft.EntityFrameworkCore;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Domain.Entities;
using Scheduling.Infrastructure.Persistence;

namespace Scheduling.Infrastructure.Repositories
{
    public class AppointmentSlotRepository : RepositoryBase<AppointmentSlot>, IAppointmentSlotRepository
    {
        private readonly AppointmentSlotContext _dbContext;
        public AppointmentSlotRepository(AppointmentSlotContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<AppointmentSlot>> GetAvailableSlotsByDateRange(DateTime dateFrom, DateTime dateTo)
        {
            var slotList = await _dbContext.AppointmentSlots
                .Where(a => a.SlotDateTime <= dateTo && 
                                        a.SlotDateTime >= dateFrom && 
                                        a.Status == SlotStatus.Available)
                .ToListAsync();
            return slotList;
        }
    }
}