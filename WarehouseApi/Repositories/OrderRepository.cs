using System.Data;
using System.Data.SqlClient;
using zadanie7.Models;

namespace zadanie7.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly string connectionString;

    public OrderRepository(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<Order> GetByProductIdAndAmount(int productId, int amount, DateTime createdAt)
    {
        using var con = new SqlConnection(connectionString);

        await con.OpenAsync();
        using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "Select * from Order WHERE IdProduct = @productId and Amount = @amount and CreatedAt < @createdAt";
        cmd.Parameters.AddWithValue("@idProduct", productId);
        cmd.Parameters.AddWithValue("amount", amount);
        cmd.Parameters.AddWithValue("@createdAt", createdAt);
        var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Order
            {
                IdOrder = reader.GetInt32(reader.GetOrdinal("IdOrder")),
                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                Amount = reader.GetInt32(reader.GetOrdinal("Amount")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                FullfilledAt = reader.GetDateTime(reader.GetOrdinal("FullfiledAt"))
            };
        }

        return null;
    }
    

    public async Task<bool> UpdateOrderFulfilledAt(int orderId)
    {
        using (var con = new SqlConnection(connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "UPDATE [Order] SET FullfilledAt = @fulfilledAt WHERE IdOrder = @orderId";
                cmd.Parameters.AddWithValue("@fulfilledAt", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
    
    

    
}