using zadanie7.Models;
using zadanie7.Repositories;

namespace zadanie7.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<Order> GetByProductIdAndAmount(int productId, int amount, DateTime createdAt)
    {
        return _orderRepository.GetByProductIdAndAmount(productId, amount, createdAt);
    }
    

    public Task<bool> UpdateOrderFulfilledAt(int orderId)
    {
        return _orderRepository.UpdateOrderFulfilledAt(orderId);
    }
}