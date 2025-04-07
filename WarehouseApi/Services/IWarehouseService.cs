using zadanie7.Models;

namespace zadanie7.Services;

public interface IWarehouseService
{
    Task<Warehouse> GetWarehouseById(int IdWarehouse);
    Task<bool> ExistsWarehouseById(int idWarehouse);
}