namespace WebhookDemo.Exceptions;

/// <summary>
/// типа DAL модель заказа
/// </summary>
/// <param name="OrderId"></param>
/// <param name="Product"></param>
/// <param name="Amount"></param>
/// <param name="DateCreated"></param>
public record Order(Guid OrderId, string Product, int Amount, DateTime DateCreated);

/// <summary>
/// запись для создания заказа
/// <remarks>должен быть в отдельном проекте</remarks>
/// </summary>
/// <param name="Product"></param>
/// <param name="Amount"></param>
public record CreateOrderRequest(string Product, int Amount);

/// <summary>
/// сервис для заказов
/// </summary>
/// <param name="logger"></param>
public class OrderService(ILogger logger)
{
    /// <summary>
    /// искомый список заказов
    /// </summary>
    ICollection<Order> _orders = [];

    /// <summary>
    /// создание нового заказа
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Order Create(CreateOrderRequest request)
    {
        var order = new Order(
            Guid.NewGuid(),
            request.Product,
            request.Amount,
            DateTime.UtcNow);

        _orders.Add(order);

        // adding order to db

        // some logging stuff

        return order;
    }

    /// <summary>
    /// получить набор имеющихся заказов
    /// </summary>
    /// <returns></returns>
    public ICollection<Order> GetAll()
        => _orders;
}
