namespace Tanner.Template.Base.Service.HttpClients;

public class FakeEndpointClient : IFakeEndpointClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<FakeEndpointClient> _logger;
    private readonly IServiceSettings _serviceSettings;

    public FakeEndpointClient(
        IHttpClientFactory httpClientFactory,
        ILogger<FakeEndpointClient> logger,
        IOptions<ServiceSettings> serviceSettings)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _serviceSettings = serviceSettings?.Value;
    }

    /// <summary>
    /// Get Data
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<ExternalServiceResponse>> GetAllData()
    {
        HttpClient client = _httpClientFactory.CreateClient(nameof(FakeEndpointClient));
        var request = new HttpRequestMessage(HttpMethod.Get, _serviceSettings.PostUri);
        HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

        var contentResponse = string.Empty;
        if (response.IsSuccessStatusCode)
        {
            contentResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return contentResponse.Deserialize<IEnumerable<ExternalServiceResponse>>();
        }

        var errorMessage = string.Format(CONSTANTS.HTTPCLIENTS_MESSAGES.SOME_ENDPOINT_ERROR, request.RequestUri, response.StatusCode);
        _logger.LogTrace(errorMessage,
            new Dictionary<string, string>
            {
                { nameof(request.RequestUri), request.RequestUri.ToString() },
                { nameof(request.Method), request.Method.ToString() },
                { nameof(client.BaseAddress), client.BaseAddress.ToString() },
                { nameof(response.StatusCode), response.StatusCode.ToString() },
                { nameof(response.ReasonPhrase), response.ReasonPhrase }
            });

        throw new FakeEndpointClientException(errorMessage);
    }
}