using NETBACKING.CORE.APPLICATION.Interfaces.Services.Generic;
using NETBACKING.CORE.APPLICATION.ViewModels.Beneficiary;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Services.Beneficiarys;

public interface IBeneficiaryService : IService<Beneficiary>
{
    Task<List<BeneficiaryViewModel>> GetByIdUserAsyncModel(string? id);
    Task AddAsyncByModel(string idCuenta, string? idUser);
    Task<BeneficiaryViewModel?> GetBeneficiaryByIdCuentaAsync(string idCuenta);
    Task<BeneficiaryViewModel?> GetBeneficiaryByIdCuentaAndbyUserId(string idCuenta, string? idUser);
}