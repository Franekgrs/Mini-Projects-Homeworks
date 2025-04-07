using WarehouseApiPrzedKolosem.DTOs;

namespace WarehouseApiPrzedKolosem.Repositories;

public interface IProduct_WarehouseRepository
{
    Task<bool> ExistsByIdOrder(WarehouseDto warehouseDto);
    Task<int> AddToProductWarehouse(WarehouseDto warehouseDto);
    Task<int> GetOrderId(WarehouseDto warehouseDto);
}