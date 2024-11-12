using System.ComponentModel.DataAnnotations;

namespace NETBACKING.CORE.APPLICATION.ViewModels.Products
{
    public class DepositViewModel
    {
        [Required(ErrorMessage = "El numero de cuenta es obligatorio")]
        [MinLength(9, ErrorMessage = "El numero de cuenta debe de tener 9 digitos")]
        public string AccountNumber { get; set; } 
        
        [Required(ErrorMessage = "El monto a depositar es obligatorio")]
        public decimal Amount { get; set; }      
       
    }
}