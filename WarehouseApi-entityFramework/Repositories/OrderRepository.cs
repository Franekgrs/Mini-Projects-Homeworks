using System.Data.SqlClient;
using WarehouseApiPrzedKolosem.DTOs;
using WarehouseApiPrzedKolosem.Exceptions;

namespace WarehouseApiPrzedKolosem.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly string connectionstring;

    public OrderRepository(IConfiguration configuration)
    {
        connectionstring = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<bool> ExistOrder(WarehouseDto warehouseDto)
    {
        if (warehouseDto == null)
        {
            throw new ArgumentNullException(nameof(warehouseDto), "WarehouseDto parameter cannot be null");
        }
        using var con = new SqlConnection(connectionstring);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText = "SELECT * from [Order] where IdProduct = @idProduct and Amount = @amount and CreatedAt < @createdAt ";
        cmd.Parameters.AddWithValue("@idProduct", warehouseDto.IdProduct);
        cmd.Parameters.AddWithValue("@amount", warehouseDto.Amount);
        cmd.Parameters.AddWithValue("@createdAt", warehouseDto.CreatedAt);
        var resultObject = await cmd.ExecuteScalarAsync();
        var result = resultObject != null ? Convert.ToInt32(resultObject) : 0;
        if (result == 0)
        {
            throw new NotFoundException("Zamowienie o podanym id i amount nie istnieje");
        }

        return true;
    }

    public async Task UpdateFullfilledAt(WarehouseDto warehouseDto)
    {
        using var con = new SqlConnection(connectionstring);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText =
            "update [Order] set FulfilledAt = @fullfilledAt where idOrder = (select o.IdOrder from [Order] o where IdProduct = @idProduct and Amount = @amount)";
        cmd.Parameters.AddWithValue("@idProduct", warehouseDto.IdProduct);
        cmd.Parameters.AddWithValue("@amount", warehouseDto.Amount);
        cmd.Parameters.AddWithValue("@fullfilledAt", DateTime.Now);
        await cmd.ExecuteNonQueryAsync();
    }
}