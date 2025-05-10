namespace WebhookDemo.Services;

/// <summary>
/// простая модель записи подписки
/// </summary>
/// <param name="EventId"></param>
/// <param name="EventName"></param>
/// <param name="EventDescription"></param>
/// <param name="DestinationUrl"></param>
/// <param name="SubscriptionDate"></param>
public record WebhookSubscription(Guid EventId, string EventName, string? EventDescription, string DestinationUrl, DateTime SubscriptionDate);

/// <summary>
/// сервис для подписки на события вебхука в памяти
/// </summary>
public sealed class InMemoryWebhookEventSubscriptionService
{
    /// <summary>
    /// искомый список подписок
    /// </summary>
    ICollection<WebhookSubscription> _events = [];
    
    /// <summary>
    /// инициализация
    /// </summary>
    /// <param name="logger"></param>
    public InMemoryWebhookEventSubscriptionService(ILogger logger)
    {
        // some logging stuff
    }

    /// <summary>
    /// подписаться на определенное событие
    /// </summary>
    /// <param name="webhookEvent"></param>
    public void Add(WebhookSubscription webhookEvent) => _events.Add(webhookEvent);

    /// <summary>
    /// получить набор подписок по названию события
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns></returns>
    public ICollection<WebhookSubscription> GetByEventName(string eventName)
        => _events.Where(e => e.EventName.Equals(eventName)).ToList();
}
