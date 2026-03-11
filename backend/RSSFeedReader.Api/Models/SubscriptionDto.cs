namespace RSSFeedReader.Api.Models;

public sealed record SubscriptionDto(
    Guid Id,
    string Url,
    DateTimeOffset CreatedAt);
