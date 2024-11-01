using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETBACKING.CORE.DOMAIN.Entities
{
    public class CashAdvance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("CreditCard")]
        public int CreditCardId { get; set; }

        [ForeignKey("DestinationAccount")]
        public int DestinationAccountId { get; set; }
        public virtual Product DestinationAccount { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El interés debe ser positivo.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Interest { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public virtual Product CreditCard { get; set; }
    }
}