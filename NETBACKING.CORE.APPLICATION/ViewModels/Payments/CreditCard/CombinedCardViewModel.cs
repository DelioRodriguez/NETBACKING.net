using NETBACKING.CORE.APPLICATION.ViewModels.Products;
using NETBACKING.CORE.DOMAIN.Entities;

namespace NETBACKING.CORE.APPLICATION.ViewModels.Payments.CreditCard;

public class CombinedCardViewModel
{
    public List<ProductViewModel> Current { get; set; }
    public List<ProductViewModel> CreditCards { get; set; }
}