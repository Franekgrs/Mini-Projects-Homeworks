using System.Data.SqlClient;
using zadanie7.Models;

namespace zadanie7.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly string connectionString;

    public ProductRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<Product> GetProductById(int idProduct)
    {
        using var con = new SqlConnection(connectionString);
        
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * from Product where IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", idProduct);
        var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Product
                {
                    IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price"))
                };
            }
        return null;
    }

    public async Task<bool> ExistsProductById(int idProduct)
    {
        using var con = new SqlConnection(connectionString);
        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT COUNT(*) FROM Product where IdProduct = @IdProduct";
        cmd.Parameters.AddWithValue("@IdProduct", idProduct);
        int productCount = (int)await cmd.ExecuteScalarAsync();
        return productCount > 0;
    }

   
}