using System.Data.SqlClient;
using zadanie7.Models;

namespace zadanie7.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly string connectionString;

    public WarehouseRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<Warehouse> GetWarehouseById(int idWarehouse)
    {
        using var con = new SqlConnection(connectionString);
        
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Warehouse WHERE IdWarehouse = @idWarehouse";
        cmd.Parameters.AddWithValue("@idWarehouse", idWarehouse);
        var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Warehouse
            {
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Address = reader.GetString(reader.GetOrdinal("Address"))
            };
        }

        return null;
    }

    public async Task<bool> ExistsWarehouseById(int idWarehouse)
    {
        using var con = new SqlConnection(connectionString);
        
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT count(*) FROM Warehouse WHERE IdWarehouse = @idWarehouse";
        cmd.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
        int warehouseCount = (int)await cmd.ExecuteScalarAsync();
        return warehouseCount > 0;
    }

}