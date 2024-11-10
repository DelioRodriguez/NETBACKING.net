using NETBACKING.CORE.APPLICATION.Interfaces.Services.Generic;
using NETBACKING.CORE.APPLICATION.ViewModels.Products;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;

public interface IProductService : IService<Product>
{
    Task<IEnumerable<ProductViewModel>> GetAllProductsByModel(string userId);
    Task CreateProduct(ProductCreateViewModel productViewModel);
    Task<Product> CreateProductofficial(ProductViewModel productViewModel);

}