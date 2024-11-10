using Microsoft.EntityFrameworkCore;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Beneficiarys;
using NETBACKING.CORE.DOMAIN.Entities;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Generic;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Beneficiarys;

public class BeneficiaryRepository : Repository<Beneficiary>, IBeneficiaryRepository
{
    private readonly AppDbContext _context;
    
    public BeneficiaryRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> BeneficiaryExistsAsync(string idCuenta)
    {
        return await _context.Beneficiaries
            .AnyAsync(b => b.AccountNumber == idCuenta);
    }
    
    public async Task<Beneficiary?> GetBeneficiaryByIdCuentaAsync(string idCuenta)
    {
        return await _context.Beneficiaries
            .FirstOrDefaultAsync(b => b.AccountNumber == idCuenta);
    }

    public async Task<Beneficiary?> GetByUserIdAndAccountNumberAsync(string? userId, string accountNumber)
    {
        return await _context.Beneficiaries
            .FirstOrDefaultAsync(b => b.ApplicationUserId == userId && b.AccountNumber == accountNumber);
    }

    public  async Task<List<Beneficiary>> GetByIdUserAsyncModel(string? id)
    {
        return await _context.Beneficiaries
            .Where(b => b.ApplicationUserId == id)
            .ToListAsync();
    }
}