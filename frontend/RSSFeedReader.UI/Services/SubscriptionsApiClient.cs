using System.Net;
using System.Net.Http.Json;
using RSSFeedReader.UI.Models;

namespace RSSFeedReader.UI.Services;

public sealed class SubscriptionsApiClient
{
    private readonly HttpClient _http;

    public SubscriptionsApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<IReadOnlyList<SubscriptionDto>> GetSubscriptionsAsync(CancellationToken cancellationToken = default)
    {
        var result = await _http.GetFromJsonAsync<List<SubscriptionDto>>(
            "subscriptions",
            cancellationToken);

        return result ?? [];
    }

    public async Task<SubscriptionDto> CreateSubscriptionAsync(string url, CancellationToken cancellationToken = default)
    {
        var response = await _http.PostAsJsonAsync(
            "subscriptions",
            new CreateSubscriptionRequest(url),
            cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var dto = await response.Content.ReadFromJsonAsync<SubscriptionDto>(cancellationToken: cancellationToken);
            return dto ?? throw new InvalidOperationException("API returned an empty response.");
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var error = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: cancellationToken);
            throw new InvalidOperationException(error?.Message ?? "Request rejected.");
        }

        response.EnsureSuccessStatusCode();
        throw new InvalidOperationException("Unexpected API response.");
    }
}
