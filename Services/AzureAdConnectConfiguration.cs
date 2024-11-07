using Microsoft.Extensions.Configuration;

namespace CBRE.Infrastructure.AzureAdClient
{
    public class AzureAdConnectConfiguration : IAzureAdConnectConfiguration
    {
        public string TenantId { get;  set; }
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }

        public AzureAdConnectConfiguration(IConfiguration configuration)
        {
            TenantId = configuration.GetValue<string>("TenantId");
            ClientId = configuration.GetValue<string>("ClientId"); 
            ClientSecret = configuration.GetValue<string>("ClientSecret");
        }
    }
}
