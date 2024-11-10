using NETBACKING.CORE.APPLICATION.ViewModels.Products;

namespace NETBACKING.CORE.APPLICATION.ViewModels.Payments.Loan;

public class CombinedLoanViewModel
{
    public List<ProductViewModel> Current { get; set; }
    public List<ProductViewModel> Loans { get; set; }
}