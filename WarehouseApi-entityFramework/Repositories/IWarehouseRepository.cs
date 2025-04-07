using WarehouseApiPrzedKolosem.DTOs;

namespace WarehouseApiPrzedKolosem.Repositories;

public interface IWarehouseRepository
{
    Task<bool> ExistWarehouse(WarehouseDto warehouseDto);
}