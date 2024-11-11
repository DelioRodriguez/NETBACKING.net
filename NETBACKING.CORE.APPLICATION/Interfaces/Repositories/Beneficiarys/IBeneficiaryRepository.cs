using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Beneficiarys;

public interface IBeneficiaryRepository : IRepository<Beneficiary>
{
    Task<Beneficiary?> GetByUserIdAndAccountNumberAsync(string? userId, string accountNumber);
    Task<List<Beneficiary>> GetByIdUserAsyncModel(string? id);
    Task<Beneficiary?> GetBeneficiaryByIdCuentaAsync(string idCuenta);
    Task<bool> BeneficiaryExistsAsync(string idCuenta);
}