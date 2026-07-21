namespace ILoggerAndLogCategory.Services
{
    public class OrderService(ILogger<OrderService> logger) : IOrderService
    {
        public object GetOrders(int orderId)
        {
            logger.LogInformation("OrderId Is Started Processing", orderId);
            return new
            {
                Id = orderId,
                TotalPrice = Random.Shared.NextDouble() * 12,
            };
        }
    }
}
