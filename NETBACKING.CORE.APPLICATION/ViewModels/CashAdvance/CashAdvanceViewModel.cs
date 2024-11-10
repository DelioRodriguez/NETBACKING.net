using NETBACKING.CORE.APPLICATION.ViewModels.Products;
using System.ComponentModel.DataAnnotations;

namespace NETBACKING.CORE.APPLICATION.ViewModels.CashAdvance
{
    public class CashAdvanceViewModel
    {
        [Required(ErrorMessage = "Seleccione una tarjeta de crrdito.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una tarjeta de credito.")]
        public int CreditCardId { get; set; }

        [Required(ErrorMessage = "Seleccione una cuenta de ahorro.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una cuenta de ahorro.")]
        public int DestinationAccountId { get; set; }

        [Required(ErrorMessage = "Ingrese el monto del avance.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        public decimal Amount { get; set; }

        public List<ProductViewModel>? CreditCards { get; set; }
        public List<ProductViewModel>? SavingsAccounts { get; set; }
    }
}
