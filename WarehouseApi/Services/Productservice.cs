using zadanie7.Models;
using zadanie7.Repositories;

namespace zadanie7.Services;

public class Productservice : IProductService
{
    private readonly IProductRepository _productRepository;

    public Productservice(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<Product> GetProductById(int id)
    {
        return _productRepository.GetProductById(id);
    }

    public Task<bool> ExistsProductById(int id)
    {
        return _productRepository.ExistsProductById(id);
    }
}