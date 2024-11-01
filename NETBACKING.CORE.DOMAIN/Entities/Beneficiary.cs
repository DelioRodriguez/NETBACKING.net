using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETBACKING.CORE.DOMAIN.Entities
{
    public class Beneficiary
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "El número de cuenta debe tener exactamente 9 dígitos.")]
        public string AccountNumber { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}