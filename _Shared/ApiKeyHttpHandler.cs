using System.Net.Http.Headers;

namespace fiap_fcg_elastic_func._Shared;

public class ApiKeyHttpHandler : DelegatingHandler
{
    private readonly string _apiKey;

    public ApiKeyHttpHandler(string apiKey)
    {
        _apiKey = apiKey;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("ApiKey", _apiKey);
        return await base.SendAsync(request, cancellationToken);
    }
}