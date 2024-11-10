using AutoMapper;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Products;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Products;
using NETBACKING.CORE.APPLICATION.Services.Generic;
using NETBACKING.CORE.APPLICATION.ViewModels.Products;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Services.Products;

public class ProductService : Service<Product>, IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    
    public ProductService(IRepository<Product> repository, IMapper mapper, IProductRepository productRepository) : base(repository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProductsByModel(string? userId)
    {
        var products = await  _productRepository.GetByUserIdAsync(userId);
        
        return products.Select(_mapper.Map<ProductViewModel>).ToList();
    }

    public async Task<ProductViewModel?> GetProductByIdentificador(string identificador)
    {
        return _mapper.Map<ProductViewModel>(await _productRepository.GetProductByIdentificador(identificador));
    }

    public async Task<IEnumerable<ProductViewModel>> GetProductsByCreditCard(string? userId)
    {
       return _mapper.Map<IEnumerable<ProductViewModel>>( await _productRepository.GetProductsByCardUser("Tarjeta de Crédito", userId));
    }

    public async Task<IEnumerable<ProductViewModel>> GetProductsBycurrentCard(string? userId)
    {
        return _mapper.Map<IEnumerable<ProductViewModel>>(
            await _productRepository.GetProductsByCardUser("Cuenta Corriente", userId));
    }

    public async Task<IEnumerable<ProductViewModel>> GetProductsByLoan(string? userId)
    {
        return _mapper.Map<IEnumerable<ProductViewModel>>(
            await _productRepository.GetProductsByCardUser("Préstamo", userId));
    }
}