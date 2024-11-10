using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Products;

public interface IProductRepository : IRepository<Product>
{ 
    Task<Product> GetPrimaryAccount(string userId);
    Task<List<Product>> GetByUserIdAsync(string userId);
    Task DeleteAsync(string productId);
    Task<Product?> GetProductByIdentificador(string identificador);
    Task<IEnumerable<Product>> GetProductsByCardUser(string productType, string? userId);
}