using NETBACKING.CORE.APPLICATION.Interfaces.Services.Generic;
using NETBACKING.CORE.APPLICATION.ViewModels.Payments.Express;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.Express;

public interface IExpressService : IService<Transaction>
{
    Task<Transaction> RealizarPagoExpressAsync(ExpressViewModel model);
    Task<Transaction> RealizarPagoBeneficiariosAsync(ExpressViewModel model);
}