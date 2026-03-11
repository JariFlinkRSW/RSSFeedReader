namespace RSSFeedReader.Api.Models;

public sealed class Subscription
{
    public required Guid Id { get; init; }

    public required string Url { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }
}
