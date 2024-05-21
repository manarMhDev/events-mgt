

using Events.Contracts.Services;
using Events.Contracts.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Events.Contracts
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddContracts(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddScoped<IFileService, FileService>();
            return services;
        }
    }
}
