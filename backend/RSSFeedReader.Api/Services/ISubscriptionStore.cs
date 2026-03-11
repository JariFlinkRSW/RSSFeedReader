using RSSFeedReader.Api.Models;

namespace RSSFeedReader.Api.Services;

public interface ISubscriptionStore
{
    IReadOnlyList<Subscription> List();

    Subscription Add(string url);
}
