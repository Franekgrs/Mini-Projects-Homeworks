using zadanie7.Models;

namespace zadanie7.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByProductIdAndAmount(int productId, int amount, DateTime createdAt);
    Task<bool> UpdateOrderFulfilledAt(int orderId);
}