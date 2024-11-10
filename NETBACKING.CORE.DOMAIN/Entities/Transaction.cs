using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETBACKING.CORE.DOMAIN.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "El tipo de transacción no puede exceder los 20 caracteres.")]
        public string TransactionType { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor que cero.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [ForeignKey("SourceAccount")]
        public int SourceAccountId { get; set; }

        [ForeignKey("DestinationAccount")]
        public int? DestinationAccountId { get; set; }

        [Required]
        public virtual Product SourceAccount { get; set; }

        public virtual Product? DestinationAccount { get; set; }
    }
}