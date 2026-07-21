using M02.OrderPaymentSystem.OrderServiceApi.Models;

namespace M02.OrderPaymentSystem.OrderServiceApi.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
    Task RemoveAsync(Order order, CancellationToken cancellationToken = default);
}
