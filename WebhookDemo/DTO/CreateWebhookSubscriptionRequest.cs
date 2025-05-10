namespace WebhookDemo.DTO;

public record CreateWebhookSubscriptionRequest(string EventName, string? EventDescription, string EventUrl);