using WarehouseApiPrzedKolosem.DTOs;
using WarehouseApiPrzedKolosem.Exceptions;
using WarehouseApiPrzedKolosem.Repositories;

namespace WarehouseApiPrzedKolosem.Services;

public class ProductWarehouseService : IProductWarehouseService
{
    private readonly IProduct_WarehouseRepository _productWarehouseRepository;
    private readonly IProductRepository _productRepository;
    private readonly IWarehouseRepository _warehouseRepository;
    private readonly IOrderRepository _orderRepository;

    public ProductWarehouseService(IProduct_WarehouseRepository productWarehouseRepository, IProductRepository productRepository, IWarehouseRepository warehouseRepository, IOrderRepository orderRepository)
    {
        _productWarehouseRepository = productWarehouseRepository;
        _productRepository = productRepository;
        _warehouseRepository = warehouseRepository;
        _orderRepository = orderRepository;
    }

    public async Task<int> AddToProductWarehouse(WarehouseDto warehouseDto)
    {
        //sprawdzamy czy istnieje produkt o podanym id
        await _productRepository.ExistProduct(warehouseDto);
        //sprawdzamy czy isniteje magazyn o podanym id
        await _warehouseRepository.ExistWarehouse(warehouseDto);
        // sprawdzamy czy istenieje order o podanym idprodcut i amount oraz CreatedAt > ..
        await _orderRepository.ExistOrder(warehouseDto);
        //sprawdzamy czy zamowienie nie zostalo juz zrealizowane
        await _productWarehouseRepository.ExistsByIdOrder(warehouseDto);
        //atkualizujemy date fullfilledAt dla zamowienia
        await _orderRepository.UpdateFullfilledAt(warehouseDto);
        //dodajemy produkt do magazynu
        var idProductWarehouse = await _productWarehouseRepository.AddToProductWarehouse(warehouseDto);
        //zwracamy id produktu w magazynie
        return idProductWarehouse;
    }
}