using NETBACKING.CORE.APPLICATION.Interfaces.Repositories;
using NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Context;

namespace NETBACKING.INFRAESTRUCTURE.PERSISTENCE.Repositories
{
    public class DashBoardRepository : IDashBoardRepository
    {
        private readonly AppDbContext _context;

        public DashBoardRepository(AppDbContext context)
        {
            _context = context;
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
            return _context.CashAdvances.Sum(ca => ca.Amount);
        }

        public decimal GetPaymentsToday()
        {
            return _context.CashAdvances.Where(ca => ca.Date.Date == DateTime.Today).Sum(ca => ca.Amount);
        }

        public int GetActiveCustomers()
        {
            return _context.Users.Count(u => u.IsActive);
        }

        public int GetInactiveCustomers()
        {
            return _context.Users.Count(u => !u.IsActive);
        }

        public int GetAssignedProducts()
        {
            return _context.Products.Count(p => p.UserId != null);
        }
    }
}