using zadanie7.Models;

namespace zadanie7.Repositories;

public interface IWarehouseRepository
{
    Task<Warehouse> GetWarehouseById(int IdWarehouse);
    Task<bool> ExistsWarehouseById(int idWarehouse);
}