using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETBACKING.CORE.DOMAIN.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "El tipo de producto no puede exceder los 20 caracteres.")]
        public string ProductType { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "El identificador único debe tener exactamente 9 caracteres.")]
        public string UniqueIdentifier { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El balance debe ser mayor o igual a cero.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }

        [Required]
        public bool IsPrimary { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El límite de crédito debe ser mayor o igual a cero.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CreditLimit { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El monto del préstamo debe ser mayor o igual a cero.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LoanAmount { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}