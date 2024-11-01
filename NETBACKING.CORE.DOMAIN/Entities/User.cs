using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETBACKING.CORE.DOMAIN.Entities
{
    public class User
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
        [MaxLength(15, ErrorMessage = "La identificación no puede exceder los 15 caracteres.")]
        public string Identification { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido.")]
        [MaxLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        public string Email { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "El nombre de usuario no puede exceder los 30 caracteres.")]
        public string Username { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "La contraseña no puede exceder los 100 caracteres.")]
        public string Password { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "El tipo de usuario no puede exceder los 20 caracteres.")]
        public string UserType { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El monto inicial debe ser mayor o igual a cero.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal InitialAmount { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
