using Microsoft.AspNetCore.Mvc;
using RSSFeedReader.Api.Models;
using RSSFeedReader.Api.Services;

namespace RSSFeedReader.Api.Controllers;

[ApiController]
[Route("api/subscriptions")]
public sealed class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionStore _store;

    public SubscriptionsController(ISubscriptionStore store)
    {
        _store = store;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<SubscriptionDto>> List()
    {
        var items = _store
            .List()
            .Select(s => new SubscriptionDto(s.Id, s.Url, s.CreatedAt))
            .ToList();

        return Ok(items);
    }

    [HttpPost]
    public ActionResult<SubscriptionDto> Create([FromBody] CreateSubscriptionRequest request)
    {
        if (request is null || string.IsNullOrWhiteSpace(request.Url))
        {
            return BadRequest(new ErrorResponse("Url is required."));
        }

        var subscription = _store.Add(request.Url.Trim());
        var dto = new SubscriptionDto(subscription.Id, subscription.Url, subscription.CreatedAt);

        return Created($"/api/subscriptions/{dto.Id}", dto);
    }
}
