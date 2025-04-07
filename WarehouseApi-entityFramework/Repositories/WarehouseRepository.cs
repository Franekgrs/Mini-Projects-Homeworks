using System.Data.SqlClient;
using WarehouseApiPrzedKolosem.DTOs;
using WarehouseApiPrzedKolosem.Exceptions;

namespace WarehouseApiPrzedKolosem.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly string connectionstring;

    public WarehouseRepository(IConfiguration configuration)
    {
        connectionstring = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<bool> ExistWarehouse(WarehouseDto warehouseDto)
    {
        if (warehouseDto == null)
        {
            throw new ArgumentNullException(nameof(warehouseDto), "WarehouseDto parameter cannot be null");
        }
        using var con = new SqlConnection(connectionstring);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText = "Select * from Warehouse where IdWarehouse = @idWarehouse";
        cmd.Parameters.AddWithValue("idWarehouse", warehouseDto.IdWarehouse);
        var result = (int)await cmd.ExecuteScalarAsync();
        if (result == 0)
        {
            throw new NotFoundException("Magazyn o id: {id} nie istnieje");
        }

        return true;
    }
}