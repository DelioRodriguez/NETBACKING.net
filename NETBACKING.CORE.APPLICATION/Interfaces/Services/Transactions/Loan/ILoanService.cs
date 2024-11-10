using NETBACKING.CORE.APPLICATION.Interfaces.Services.Generic;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.Interfaces.Services.Transactions.Loan;

public interface ILoanService : IService<Transaction>{
    Task<bool> PayLoanCardAsync(string loanAccount, string originAccount, decimal paymentAmount);
}