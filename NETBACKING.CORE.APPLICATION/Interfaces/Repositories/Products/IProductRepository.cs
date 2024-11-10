using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Products;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetByUserIdAsync(string? userId);
    Task<Product?> GetProductByIdentificador(string identificador);
    Task<IEnumerable<Product>> GetProductsByCardUser(string productType, string? userId);
}