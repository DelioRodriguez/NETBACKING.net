using NETBACKING.CORE.APPLICATION.Interfaces.Repositories;
using NETBACKING.INFRAESTRUCTURE.IDENTITY.Context;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories.DashBoard
{
    public class DashBoardRepository : IDashBoardRepository
    {
        private readonly AppDbContext _context;
        private readonly IdentityContext identityContext;

        public DashBoardRepository(AppDbContext context, IdentityContext context1)
        {
            _context = context;
           identityContext = context1;
        }
        public int GetTotalTransactions()
        {
            return _context.Transactions.Count();
        }

        public int GetTransactionsToday()
        {
            return _context.Transactions.Count(t => t.Date.Date == DateTime.Today);
        }

        public decimal GetTotalPayments()
        {
            return _context.Transactions.Sum(ca => ca.Amount);
        }

        public decimal GetPaymentsToday()
        {
            return _context.Transactions.Where(ca => ca.Date.Date == DateTime.Today).Sum(ca => ca.Amount);
        }

        public int GetActiveCustomers()
        {
            return identityContext.Users.Count(u => u.IsActive);
        }

        public int GetInactiveCustomers()
        {
            return identityContext.Users.Count(u => !u.IsActive);
        }

        public int GetAssignedProducts()
        {
            return _context.Products.Count(p => p.ApplicationUserId != null);
        }
    }
}