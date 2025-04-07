using zadanie7.Models;

namespace zadanie7.Services;

public interface IProductWarehouseService
{
    Task<int> AddProductToWarehouse(ProductWarehouse productWarehouse);
}