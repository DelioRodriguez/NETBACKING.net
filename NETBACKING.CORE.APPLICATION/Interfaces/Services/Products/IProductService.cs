using NETBACKING.CORE.APPLICATION.Interfaces.Services.Generic;
using NETBACKING.CORE.APPLICATION.ViewModels.Products;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;

public interface IProductService : IService<Product>
{  
    Task CreateProduct(ProductCreateViewModel productViewModel);
    Task<Product> CreateProductofficial(ProductViewModel productViewModel);
    Task<IEnumerable<ProductViewModel>> GetAllProductsByModel(string? userId);
    Task<ProductViewModel?> GetProductByIdentificador(string identificador);
    Task<IEnumerable<ProductViewModel>> GetProductsByCreditCard(string? userId);
    Task<IEnumerable<ProductViewModel>> GetProductsBycurrentCard(string? userId);
    Task<IEnumerable<ProductViewModel>> GetProductsByLoan(string? userId);
}