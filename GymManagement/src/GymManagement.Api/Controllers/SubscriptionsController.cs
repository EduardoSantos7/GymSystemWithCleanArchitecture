using GymManagement.Application.Services;
using GymManagement.Contracts.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionsService _subscriptionService;

    public SubscriptionsController(ISubscriptionsService subscriptionsService)
    {
        _subscriptionService = subscriptionsService;
    }

    [HttpPost]
    public IActionResult CreateSubscription(CreateSubscriptionRequest request)
    {
        var subscriptionId =  _subscriptionService.CreateSubscription(
            request.SubscriptionType.ToString(),
            request.AdminId);
    
        var response = new SubscriptionResponse(
            subscriptionId,
            request.SubscriptionType);

        return Ok(response);
    }
}