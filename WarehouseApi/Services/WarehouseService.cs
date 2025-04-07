using zadanie7.Models;
using zadanie7.Repositories;

namespace zadanie7.Services;

public class WarehouseService : IWarehouseService
{

    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public Task<Warehouse> GetWarehouseById(int IdWarehouse)
    {
        return _warehouseRepository.GetWarehouseById(IdWarehouse);
    }

    public Task<bool> ExistsWarehouseById(int idWarehouse)
    {
        return _warehouseRepository.ExistsWarehouseById(idWarehouse);
    }
}