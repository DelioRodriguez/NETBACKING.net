using Microsoft.EntityFrameworkCore;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Transactions.Transactions;
using NETBACKING.CORE.DOMAIN.Entities;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Generic;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Transactions.Transactions
{
    public class TransactionsRepository : Repository<Transaction>, ITransactionsRepository
    {
        private readonly AppDbContext _context;
        public TransactionsRepository(AppDbContext appDbContext) : base (appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<Transaction>> GetTransactionsByUserAsync(string userId)
        {
            var transactions = await _context.Transactions
        .Include(t => t.SourceAccount)
        .Include(t => t.DestinationAccount)
        .Where(t => t.SourceAccount.ApplicationUserId == userId || (t.DestinationAccount != null && t.DestinationAccount.ApplicationUserId == userId))
        .OrderByDescending(t => t.Date)
        .ToListAsync();

            return transactions;
        }
    }
}
