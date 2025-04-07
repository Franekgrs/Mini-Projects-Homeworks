using zadanie7.Models;

namespace zadanie7.Services;

public interface IOrderService
{
    public Task<Order> GetByProductIdAndAmount(int productId, int amount, DateTime createdAt);

    Task<bool> UpdateOrderFulfilledAt(int orderId);
}