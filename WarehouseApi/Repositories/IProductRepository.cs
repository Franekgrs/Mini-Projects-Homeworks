using zadanie7.Models;

namespace zadanie7.Repositories;

public interface IProductRepository
{
    Task<Product> GetProductById(int id);
    Task<bool> ExistsProductById(int id);
}