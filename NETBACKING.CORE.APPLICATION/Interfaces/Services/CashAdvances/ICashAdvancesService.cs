using NETBACKING.CORE.APPLICATION.Interfaces.Services.Generic;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Services.CashAdvances
{
    public interface ICashAdvancesService : IService<CashAdvance>
    {
        Task<bool> ProcessCashAdvance(int creditCardId, int destinationAccountId, decimal amount);
    }
}
