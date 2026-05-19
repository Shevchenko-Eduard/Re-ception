using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace LibWeb.Entity;

public class KeycloakSchema(IConfiguration configuration)
{
    private readonly IConfigurationSection _kcSettings = configuration.GetSection("Keycloak");

    public string AuthServerUrl => _kcSettings["AuthServerUrl"] ?? throw new InvalidOperationException("Keycloak:AuthServerUrl is missing");
    public string Realm => _kcSettings["Realm"] ?? throw new InvalidOperationException("Keycloak:Realm is missing");
    public string Resource => _kcSettings["Resource"] ?? throw new InvalidOperationException("Keycloak:Resource is missing");
    public string Secret => _kcSettings["Secret"] ?? throw new InvalidOperationException("Keycloak:Secret is missing");
    public string SslRequired => _kcSettings["SslRequired"] ?? "none";
    public bool VerifyTokenAudience => bool.TryParse(_kcSettings["VerifyTokenAudience"], out var verify) && verify;

    public async Task<string> AuthorizationEndpoint()
    {
        return (await OidcConfigurationAsync())["authorization_endpoint"].ToString()
            ?? throw new Exception("authorization_endpoint is missing in OIDC configuration");
    }
    public async Task<string> TokenEndpoint()
    {
        return (await OidcConfigurationAsync())["token_endpoint"].ToString()
            ?? throw new Exception("token_endpoint is missing in OIDC configuration");
    }

    private Dictionary<string, object>? _oidcConfig = null;

    public async Task<Dictionary<string, object>> OidcConfigurationAsync()
    {
        if (_oidcConfig != null)
        {
            return _oidcConfig;
        }

        var authorityUrl = $"{AuthServerUrl}/realms/{Realm}";
        var discoveryEndpoint = $"{authorityUrl}/.well-known/openid-configuration";

        HttpClientHandler handler = new()
        {
            ServerCertificateCustomValidationCallback = // Accept all certificates (for development purposes only)
                (sender, cert, chain, sslPolicyErrors) => true
        };

        using HttpClient httpClient = new(handler);

        HttpResponseMessage response = await httpClient.GetAsync(discoveryEndpoint);
        response.EnsureSuccessStatusCode();

        string jsonString = await response.Content.ReadAsStringAsync();
        var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString)
            ?? throw new Exception("Failed to deserialize OIDC configuration");

        _oidcConfig = dict;

        return dict;
    }
}