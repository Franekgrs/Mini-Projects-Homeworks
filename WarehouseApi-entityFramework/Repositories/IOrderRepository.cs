using WarehouseApiPrzedKolosem.DTOs;

namespace WarehouseApiPrzedKolosem.Repositories;

public interface IOrderRepository
{
    Task<bool> ExistOrder(WarehouseDto warehouseDto);
    Task UpdateFullfilledAt(WarehouseDto warehouseDto);
}