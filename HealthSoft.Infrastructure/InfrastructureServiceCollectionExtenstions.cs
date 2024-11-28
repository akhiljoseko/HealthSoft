using HealthSoft.Core.RepositoryInterfaces;
using HealthSoft.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HealthSoft.Infrastructure
{
    public static class InfrastructureServiceCollectionExtenstions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) 
        {
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();

            return services;
        }
    }
}
