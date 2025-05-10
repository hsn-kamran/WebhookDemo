using WebhookDemo;
using WebhookDemo.DTO;
using WebhookDemo.Exceptions;
using WebhookDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// сервис для заказов
builder.Services.AddSingleton<OrderService>();

// сервис для регистрации события вебхука
builder.Services.AddSingleton<InMemoryWebhookEventSubscriptionService>();
// сервис для отправки post запросов к указанным адресатам
builder.Services.AddHttpClient<WebhookDispatcher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost("webhook/register", (
    CreateWebhookSubscriptionRequest createRequest, 
    InMemoryWebhookEventSubscriptionService registerService) =>
{
    var subscription = new WebhookSubscription(
        Guid.NewGuid(),
        createRequest.EventName,
        createRequest.EventDescription,
        createRequest.EventUrl,
        DateTime.UtcNow
        );

    registerService.Add(subscription);

    return Results.Ok(subscription);
});

app.MapPost("orders/create", async (
    CreateOrderRequest createOrderRequest,
    OrderService orderService,
    WebhookDispatcher dispatcher) =>
{
    var order = orderService.Create(createOrderRequest);

    // отправляем всем подписчикам уведомление о создании заказа
    await dispatcher.DispatchAsync("order.created", order);

    return Results.Ok(order);
});


app.Run();
