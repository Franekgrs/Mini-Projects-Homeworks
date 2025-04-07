using System.Data.SqlClient;
using zadanie7.Models;

namespace zadanie7.Repositories;

public class ProductWarehouseRepository : IProductWarehouseRepository
{
    private readonly string connectionString;

    public ProductWarehouseRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }


    public async Task<bool> ExistsOrderById(int idOrder)
    {
        using var con = new SqlConnection(connectionString);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT count(*) from Product_Warehouse where IdOrder = @IdOrder";
        cmd.Parameters.AddWithValue("@IdOrder", idOrder);
        var orderCount = (int)await cmd.ExecuteScalarAsync();
        return orderCount > 0;
    }

    public async Task<int> AddProductToWarehouse(ProductWarehouse productWarehouse)
    {
        using var con = new SqlConnection(connectionString);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText =
            "INSERT INTO Product_WarehouseIdWarehouse( IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt)";
        cmd.Parameters.AddWithValue("@IdWarehouse", productWarehouse.IdWarehouse);
        cmd.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);
        cmd.Parameters.AddWithValue("@IdOrder", productWarehouse.IdOrder);
        cmd.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
        cmd.Parameters.AddWithValue("@Price", productWarehouse.Price);
        cmd.Parameters.AddWithValue("@CretedAt", productWarehouse.CreatedAt);
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    
}