using NETBACKING.CORE.APPLICATION.ViewModels.Products;
using System.ComponentModel.DataAnnotations;

namespace NETBACKING.CORE.APPLICATION.ViewModels.SingleTransfer
{
    public class TransferViewModel
    {
        [Required(ErrorMessage = "Seleccione una cuenta de origen.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una cuenta de origen.")]
        public int SourceAccountId { get; set; }

        [Required(ErrorMessage = "Seleccione una cuenta de destino.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una cuenta de destino.")]
        public int DestinationAccountId { get; set; }

        [Required(ErrorMessage = "Ingrese el monto a transferir.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        public decimal Amount { get; set; }

        public List<ProductViewModel> Accounts { get; set; } = new List<ProductViewModel>();
    }
}
