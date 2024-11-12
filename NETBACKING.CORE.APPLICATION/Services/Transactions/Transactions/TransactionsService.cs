using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Generic;
using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Transactions.Transactions;
using NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.Transactions;
using NETBACKING.CORE.APPLICATION.Services.Generic;
using NETBACKING.CORE.APPLICATION.ViewModels.Transactions;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Services.Transactions.Transactions
{
    public class TransactionsService : Service<Transaction>, ITransactionsService
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionsService(IRepository<Transaction> repository, ITransactionsRepository transactionsRepository) : base(repository)
        {
            _transactionsRepository = transactionsRepository;
        }
        public async Task<List<TransactionHistoryViewModel>> GetTransactionsByAccountAsync(string userId)
        {
            var transactions = await _transactionsRepository.GetTransactionsByUserAsync(userId);

            var transactionHistory = transactions.Select(transaction => new TransactionHistoryViewModel
            {
                Date = transaction.Date,
                TransactionType = transaction.TransactionType,
                SourceAccountIdentifier = transaction.SourceAccount?.UniqueIdentifier,
                DestinationAccountIdentifier = transaction.DestinationAccount?.UniqueIdentifier ?? "No aplica",
                Amount = transaction.Amount
            }).ToList();

            return transactionHistory;


            //var transactions = await _transactionsRepository.GetTransactionsByUserAsync(userId);

            //var transactionHistory = transactions.Select(transaction => new TransactionHistoryViewModel
            //{
            //    Date = transaction.Date,
            //    TransactionType = transaction.TransactionType,
            //    SourceAccountIdentifier = transaction.SourceAccountId.ToString(),
            //    DestinationAccountIdentifier = transaction.DestinationAccountId?.ToString() ?? "No aplica",
            //    Amount = transaction.Amount
            //}).ToList();

            //return transactionHistory;
        }
    }
}
