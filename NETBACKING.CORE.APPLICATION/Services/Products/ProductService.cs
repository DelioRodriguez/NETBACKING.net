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
    
    private async Task<string> GenerateUniqueIdentifierAsync()
    {
        string uniqueIdentifier;
        bool exists;

        do
        {

            uniqueIdentifier = new Random().Next(100000000, 999999999).ToString();
            exists = (await _productRepository.GetAllAsync())
                .Any(p => p.UniqueIdentifier == uniqueIdentifier);

        } while (exists);

        return uniqueIdentifier;
    }

    public  async Task DepositToAccount(string productId, decimal amount)
    {
        var product = await _productRepository.GetProductByIdentificador(productId);
        
        if (product == null)
            throw new Exception("Producto no encontrado.");

        product.Balance += amount;

       
        await _productRepository.UpdateAsync(product);
    }

    public async Task CreateProduct(ProductCreateViewModel productViewModel)
    {
       
        var product = _mapper.Map<Product>(productViewModel);
        product.UniqueIdentifier = await GenerateUniqueIdentifierAsync();
        await _productRepository.AddAsync(product);
    }

    public async Task DeleteProduct(string productId)
    {
  
            await _productRepository.DeleteAsync(productId);
        
    }
    public async Task TransferToPrimaryAccount(string userId, decimal amount)
    {
        var primaryAccount = await _productRepository.GetPrimaryAccount(userId);

        primaryAccount.Balance += amount;
        await _productRepository.UpdateAsync(primaryAccount);
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
       return _mapper.Map<IEnumerable<ProductViewModel>>( await _productRepository.GetProductsByCardUser("tarjetacredito", userId));
    }

    public async Task<IEnumerable<ProductViewModel>> GetProductsBycurrentCard(string? userId)
    {
        return _mapper.Map<IEnumerable<ProductViewModel>>(
            await _productRepository.GetProductsByCardUser("CuentaAhorro", userId));
    }

    public async Task<IEnumerable<ProductViewModel>> GetProductsByLoan(string? userId)
    {
        return _mapper.Map<IEnumerable<ProductViewModel>>(
            await _productRepository.GetProductsByCardUser("Prestamo", userId));
    }
}