using NETBACKING.CORE.APPLICATION.Interfaces.Services.Generic;
using NETBACKING.CORE.APPLICATION.ViewModels.Transactions;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.Transactions
{
    public interface ITransactionsService : IService<Transaction>
    {
        Task<List<TransactionHistoryViewModel>> GetTransactionsByAccountAsync(string userId);
    }
}
