using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Beneficiarys;

public interface IBeneficiaryRepository : IRepository<Beneficiary>
{
    public Task<bool> BeneficiaryExistsAsync(string idCuenta);
    Task<List<Beneficiary>> GetByIdUserAsyncModel(string? id);
}