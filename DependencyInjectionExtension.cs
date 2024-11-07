using Microsoft.Extensions.DependencyInjection;

namespace CBRE.Infrastructure.AzureAdClient
{
    public static class DependencyInjectionExtension
    {
        public static void AddAzureAdClient(this IServiceCollection services, IAzureAdConnectConfiguration config)
        {
            services.AddSingleton(config);

            services.AddTransient<IAzureADConnectClient, AzureAdConnectClient>();

        }
    }
}