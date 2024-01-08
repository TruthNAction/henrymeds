using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Infrastructure.Persistence;
using Scheduling.Infrastructure.Repositories;

namespace Scheduling.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppointmentSlotContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SchedulingConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IAppointmentSlotRepository, AppointmentSlotRepository>();

            return services;
        }
    }
}