using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System_Uznawania_Przychodów.DTOs.Subscriptions;
using System_Uznawania_Przychodów.Exceptions;
using System_Uznawania_Przychodów.Services;

namespace System_Uznawania_Przychodów.Controllers;

[Route("api/{controller}")]
[ApiController]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionsController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddSubscription(AddSubscriptionDto addSubscriptionDto)
    {
        try
        {
            await _subscriptionService.AddSubscription(addSubscriptionDto);
            return Created();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("{id}")]
    public async Task<IActionResult> AddPaymentForSubscription(string id, AddPaymentForSubscriptionDto addPaymentForSubscriptionDto)
    {
        try
        {
            await _subscriptionService.AddPaymentForSubscription(int.Parse(id), addPaymentForSubscriptionDto);
            return Created();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
    }
}