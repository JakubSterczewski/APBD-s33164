using System_Uznawania_Przychodów.DTOs.Subscriptions;

namespace System_Uznawania_Przychodów.Services;

public interface ISubscriptionService
{
    Task<int> AddSubscription(AddSubscriptionDto addSubscriptionDto);
    Task AddPaymentForSubscription(int id, AddPaymentForSubscriptionDto addPaymentForSubscriptionDto);
}