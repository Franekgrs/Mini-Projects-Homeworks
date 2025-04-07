using System.Data.SqlClient;
using WarehouseApiPrzedKolosem.DTOs;
using WarehouseApiPrzedKolosem.Exceptions;

namespace WarehouseApiPrzedKolosem.Repositories;

public class Product_WarehouseRepository : IProduct_WarehouseRepository
{
    private readonly string connectionstring;

    public Product_WarehouseRepository(IConfiguration configuration)
    {
        connectionstring = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<bool> ExistsByIdOrder(WarehouseDto warehouseDto)
    {
        if (warehouseDto == null)
        {
            throw new ArgumentNullException(nameof(warehouseDto), "WarehouseDto parameter cannot be null");
        }
        int idOrder = await GetOrderId(warehouseDto);
        using var con = new SqlConnection(connectionstring);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText =
            "Select * from Product_Warehouse where IdOrder = @idOrder";
        cmd.Parameters.AddWithValue("@idOrder", idOrder);
        var resultObject = await cmd.ExecuteScalarAsync();
        var result = resultObject != null ? Convert.ToInt32(resultObject) : 0;
        if (result > 0)
        {
            throw new NotFoundException("Zamowienie o podanym id zostalo juz zrealizowane");
        }

        return true;
    }

    public async Task<int> AddToProductWarehouse(WarehouseDto warehouseDto)
    {
        int idOrder = await GetOrderId(warehouseDto);
        using var con = new SqlConnection(connectionstring);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = @"Insert into Product_warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) 
                            output inserted.IdProductWarehouse
                            values (@idWarehouse, @idProduct, @idOrder, @amount, @price, @createdAt)";
        cmd.Parameters.AddWithValue("@idWarehouse", warehouseDto.IdWarehouse);
        cmd.Parameters.AddWithValue("@idProduct", warehouseDto.IdProduct);
        cmd.Parameters.AddWithValue("@idOrder", idOrder);
        cmd.Parameters.AddWithValue("@amount", warehouseDto.Amount);
        cmd.Parameters.AddWithValue("@price", warehouseDto.Amount);
        cmd.Parameters.AddWithValue("@createdAt", DateTime.Now);
        var result = (int)await cmd.ExecuteScalarAsync();
        return result;
    }

    public async Task<int> GetOrderId(WarehouseDto warehouseDto)
    {
        using var con = new SqlConnection(connectionstring);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText = "Select IdOrder from [Order] where IdProduct = @idProduct and Amount = @amount";
        cmd.Parameters.AddWithValue("@idProduct", warehouseDto.IdProduct);
        cmd.Parameters.AddWithValue("@amount", warehouseDto.Amount);
        var result = (int)await cmd.ExecuteScalarAsync();
        return result;
    }
}