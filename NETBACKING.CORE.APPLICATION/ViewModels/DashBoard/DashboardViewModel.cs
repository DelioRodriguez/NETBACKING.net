namespace NETBACKING.CORE.APPLICATION.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalTransactions { get; set; }
        public int TransactionsToday { get; set; }
        public decimal TotalPayments { get; set; }
        public decimal PaymentsToday { get; set; }
        public int ActiveCustomers { get; set; }
        public int InactiveCustomers { get; set; }
        public int AssignedProducts { get; set; }
    }
}