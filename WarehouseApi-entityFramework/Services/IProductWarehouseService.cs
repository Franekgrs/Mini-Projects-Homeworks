using WarehouseApiPrzedKolosem.DTOs;

namespace WarehouseApiPrzedKolosem.Services;

public interface IProductWarehouseService
{
    Task<int> AddToProductWarehouse(WarehouseDto warehouseDto);
}