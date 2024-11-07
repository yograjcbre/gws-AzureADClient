
namespace CBRE.Infrastructure.AzureAdClient
{
    public interface IAzureAdConnectConfiguration
    {
        public string TenantId { get; }
        public string ClientId { get; }
        public string ClientSecret { get; }
    }
}
