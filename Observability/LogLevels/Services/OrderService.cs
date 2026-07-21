using Microsoft.Extensions.Logging;

namespace LogLevels.Services
{
    public class OrderService(ILogger<OrderService> logger) : IOrderService
    {
        public object GetOrders(int orderId)
        {
            logger.LogTrace("I'm Log Trace {orderId}", orderId);
            logger.LogDebug("I'm Log Debug {orderId}", orderId);
            logger.LogInformation("I'm Log Information {orderId}", orderId);
            logger.LogWarning("I'm Log Warning {orderId}", orderId);
            logger.LogError("I'm Log Error {orderId}", orderId);
            logger.LogCritical("I'm Log Critical {orderId}", orderId);

            return new
            {
                Id = orderId,
                TotalPrice = Random.Shared.NextDouble() * 12,
            };
        }
    }
}
