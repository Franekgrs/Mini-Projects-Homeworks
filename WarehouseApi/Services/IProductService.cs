using zadanie7.Models;

namespace zadanie7.Services;

public interface IProductService
{
    Task<Product> GetProductById(int id);
    Task<bool> ExistsProductById(int id);
}
