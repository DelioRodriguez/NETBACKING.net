using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Transactions.Transactions
{
    public interface ITransactionsRepository : IRepository<Transaction>
    {
        Task<List<Transaction>> GetTransactionsByUserAsync(string userId);
    }
}
