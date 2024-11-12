
namespace NETBACKING.CORE.APPLICATION.ViewModels.Transactions
{
    public class TransactionHistoryViewModel
    {
        public DateTime Date { get; set; }
        public string? TransactionType { get; set; }
        public string? SourceAccountIdentifier { get; set; }
        public string? DestinationAccountIdentifier { get; set; }
        public decimal Amount { get; set; }

    }
}
