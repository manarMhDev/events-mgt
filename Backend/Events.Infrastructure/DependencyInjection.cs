

using Events.Application.Common.Interfaces.Authentication;
using Events.Application.Common.Interfaces.Services;
using Events.Infrastructure.Authentication;
using Events.Infrastructure.Repositories;
using Events.Infrastructure.Services;
using Events.Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Events.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUnitOfWork, UnitOfWorkBase>();
            return services;
        }
    }
}
