using NETBACKING.CORE.APPLICATION.Interfaces.Repositories.Transactions.Loan;
using NETBACKING.CORE.DOMAIN.Entities;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Generic;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.Transactions.Loan;

public class LoanRepository : Repository<Transaction>, ILoanRepository
{
    public LoanRepository(AppDbContext context) : base(context)
    {
    }
}