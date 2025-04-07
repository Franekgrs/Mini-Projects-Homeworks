using zadanie7.Models;
using zadanie7.Repositories;

namespace zadanie7.Services;

public class ProductWarehouseService : IProductWarehouseService
{

    private readonly IProductWarehouseRepository _productWarehouseRepository;
    private readonly IProductService _productService;
    private readonly IWarehouseService _warehouseService;
    private readonly IOrderService _orderService;

    public ProductWarehouseService(IProductWarehouseRepository productWarehouseRepository, IProductService productService, IWarehouseService warehouseService, IOrderService orderService)
    {
        _productWarehouseRepository = productWarehouseRepository;
        _productService = productService;
        _warehouseService = warehouseService;
        _orderService = orderService;
    }
    

    public async Task<int> AddProductToWarehouse(ProductWarehouse productWarehouse)
    {
        // sprawdzany czy produkt istnieje
        var product = await _productService.GetProductById(productWarehouse.IdProduct);
        if (product == null)
        {
            throw new Exception("Podany produkt nie istnieje");
        }
        // sprawdzany czy magazyn istnieje
        var warehouse = await _warehouseService.GetWarehouseById(productWarehouse.IdWarehouse);
        if (warehouse == null)
        {
            throw new Exception("Podany magazyn nie istnieje");
        }
        // sprawdzany czy zamowienie istnieje
        var order = await _orderService.GetByProductIdAndAmount(productWarehouse.IdProduct, productWarehouse.Amount, productWarehouse.CreatedAt);
        if (order == null)
        {
            throw new Exception("brak zamówniena na dany produkt");
        }
        // sprawdzany czy zamowienie nie zostalo juz zrealizowane
        if (order.FullfilledAt != null)
        {
            throw new Exception("Zamowienie zostało juz zrealizowane");
        }
        // aktualizujemy date zrealizowanai zamowienia
        await _orderService.UpdateOrderFulfilledAt(order.IdOrder);
        
        //dodajemy produkt do magazynu
        productWarehouse.Price = product.Price * productWarehouse.Amount;
        productWarehouse.CreatedAt = DateTime.UtcNow;
        return await _productWarehouseRepository.AddProductToWarehouse(productWarehouse);
        
    }
}