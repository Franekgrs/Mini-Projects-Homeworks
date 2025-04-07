using zadanie7.Models;

namespace zadanie7.Repositories;

public interface IProductWarehouseRepository
{
    public Task<bool> ExistsOrderById(int idOrder);
    Task<int> AddProductToWarehouse(ProductWarehouse productWarehouse);
}