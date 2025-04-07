using System.Data.SqlClient;
using WarehouseApiPrzedKolosem.DTOs;
using WarehouseApiPrzedKolosem.Exceptions;

namespace WarehouseApiPrzedKolosem.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly string connectionstring;

    public ProductRepository(IConfiguration configuration)
    {
        connectionstring = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<bool> ExistProduct(WarehouseDto warehouseDto)
    {
        if (warehouseDto == null)
        {
            throw new ArgumentNullException(nameof(warehouseDto), "WarehouseDto parameter cannot be null");
        }
        
        using var con = new SqlConnection(connectionstring);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText = "Select * from Product where IdProduct = @idProduct";
        cmd.Parameters.AddWithValue("idProduct", warehouseDto.IdProduct);
        var resultObject = await cmd.ExecuteScalarAsync();
        var result = resultObject != null ? Convert.ToInt32(resultObject) : 0;
        if (result == 0)
        {
            throw new NotFoundException("Produkt o id {id} nie istnieje");
        }

        return true;
    }
}