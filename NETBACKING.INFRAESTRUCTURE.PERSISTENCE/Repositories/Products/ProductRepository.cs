using Microsoft.EntityFrameworkCore;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Products;
using NETBACKING.CORE.DOMAIN.Entities;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Generic;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Products;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly AppDbContext _context;
    
    public ProductRepository(AppDbContext context) : base(context)
    {
        this._context = context;
    }

    public async Task<List<Product>> GetByUserIdAsync(string? userId)
    {
        return await _context.Products
            .Where(p => p.ApplicationUserId == userId)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdentificador(string identificador)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.UniqueIdentifier == identificador)!;
    }

    public async Task<IEnumerable<Product>> GetProductsByCardUser(string productType, string? userId)
    {
        return await _context.Products
            .Where(p => p.ApplicationUserId == userId &&
                 p.ProductType == productType)
            .ToListAsync();
    }
    
}