namespace CBRE.Infrastructure.AzureAdClient;

public interface IAzureADConnectClient
{
    Task<List<AzureAdUser>> GetAllUsersAsync();
    Task<AzureAdUser> GetUserAsync(string userEmail);
}
