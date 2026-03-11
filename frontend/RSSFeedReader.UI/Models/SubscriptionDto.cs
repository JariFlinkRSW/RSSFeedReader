namespace RSSFeedReader.UI.Models;

public sealed record SubscriptionDto(
    Guid Id,
    string Url,
    DateTimeOffset CreatedAt);
