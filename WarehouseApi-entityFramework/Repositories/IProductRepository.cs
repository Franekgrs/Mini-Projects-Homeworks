using WarehouseApiPrzedKolosem.DTOs;

namespace WarehouseApiPrzedKolosem.Repositories;

public interface IProductRepository
{
    Task<bool> ExistProduct(WarehouseDto warehouseDto);
}