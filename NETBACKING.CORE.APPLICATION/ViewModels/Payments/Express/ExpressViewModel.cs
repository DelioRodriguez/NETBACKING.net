using System.ComponentModel.DataAnnotations;

namespace NETBACKING.CORE.APPLICATION.ViewModels.Payments.Express;

public class ExpressViewModel
{
        public string AccountNumber { get; set; }
    
        [Required(ErrorMessage = "El monto es requerido.")]
        [Range(1, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        public decimal PaymentAmount { get; set; }
    
        [Required(ErrorMessage = "La cuenta de origen es requerida.")]
        public string OriginAccount { get; set; }
}