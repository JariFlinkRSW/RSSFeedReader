using RSSFeedReader.Api.Models;

namespace RSSFeedReader.Api.Services;

public sealed class InMemorySubscriptionStore : ISubscriptionStore
{
    private readonly List<Subscription> _subscriptions = new();
    private readonly object _lock = new();

    public IReadOnlyList<Subscription> List()
    {
        lock (_lock)
        {
            return _subscriptions.ToList();
        }
    }

    public Subscription Add(string url)
    {
        var subscription = new Subscription
        {
            Id = Guid.NewGuid(),
            Url = url,
            CreatedAt = DateTimeOffset.UtcNow,
        };

        lock (_lock)
        {
            _subscriptions.Add(subscription);
        }

        return subscription;
    }
}
