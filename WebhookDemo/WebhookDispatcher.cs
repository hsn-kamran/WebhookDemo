using WebhookDemo.Exceptions;
using WebhookDemo.Services;

namespace WebhookDemo;


/// <summary>
/// диспатчер для отправки данных на подписанные вебхуки
/// </summary>
/// <param name="httpClient"></param>
/// <param name="eventSubscriptionService"></param>
public sealed class WebhookDispatcher(HttpClient httpClient, 
    InMemoryWebhookEventSubscriptionService eventSubscriptionService)
{
    /// <summary>
    /// отправить данные всем подписикам на событие
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="payload"></param>
    /// <returns></returns>
    public async Task DispatchAsync(string eventName, object payload)
    {
        var subscriptions = eventSubscriptionService.GetByEventName(eventName);

        try
        {
            foreach (var subscription in subscriptions)
            {
                var request = new
                {
                    Id = Guid.NewGuid(),
                    subscription.EventName,
                    SubscriptionId = subscription.EventId,
                    TimeStamp = DateTime.UtcNow,
                    Data = payload
                };

                var response = await httpClient.PostAsJsonAsync(subscription.DestinationUrl, request);

                if (!response.IsSuccessStatusCode)
                    throw new WebhookDispatchException(eventName, subscription.DestinationUrl);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

}
