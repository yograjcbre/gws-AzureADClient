using Azure.Identity;
using Microsoft.Graph;

namespace CBRE.Infrastructure.AzureAdClient;

public class AzureAdConnectClient : IAzureADConnectClient
{
    private readonly GraphServiceClient _graphServiceClient;

    public readonly static string[] UserQueryParams = new string[] { "GivenName", "Surname", "mail", "mobilePhone", "jobTitle" };


    public AzureAdConnectClient(IAzureAdConnectConfiguration configuration)
    {
        var clientSecretCredential = new ClientSecretCredential(configuration.TenantId, configuration.ClientId, configuration.ClientSecret);
        _graphServiceClient = new GraphServiceClient(clientSecretCredential);
    }

    public async Task<List<AzureAdUser>> GetAllUsersAsync()
    {
        var userResult = await _graphServiceClient
            .Users            
            .GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Select = UserQueryParams;
            });

        return userResult.Value.Select(array => new AzureAdUser
        {
            FirstName = array?.GivenName ?? string.Empty,
            LastName = array?.Surname ?? string.Empty,
            Email = array?.Mail ?? string.Empty,
            Phone = array?.MobilePhone ?? string.Empty,
            Position = array?.JobTitle ?? string.Empty
        }).ToList();
    }

    public async Task<AzureAdUser> GetUserAsync(string userEmail)
    {
        var userResult = await _graphServiceClient
            .Users[userEmail]
            .GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Select = UserQueryParams;
            });

        if(userResult?.Mail == null) {
            throw new KeyNotFoundException($"User with Email: {userEmail} not found in Azure AD");
        }

        return new AzureAdUser {
            Email = userResult.Mail,
            LastName = userResult.Surname ?? string.Empty,
            FirstName = userResult.GivenName ?? string.Empty,
            Phone = userResult.MobilePhone ?? string.Empty,
            Position = userResult.JobTitle ?? string.Empty
        };
    }
}
