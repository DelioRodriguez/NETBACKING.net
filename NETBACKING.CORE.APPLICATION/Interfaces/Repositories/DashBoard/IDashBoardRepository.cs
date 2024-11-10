namespace NETBACKING.CORE.APPLICATION.Interfaces.Repositories
{
    public interface IDashBoardRepository
    {
        int GetTotalTransactions();
        int GetTransactionsToday();
        decimal GetTotalPayments();
        decimal GetPaymentsToday();
        int GetActiveCustomers();
        int GetInactiveCustomers();
        int GetAssignedProducts();

    }
}