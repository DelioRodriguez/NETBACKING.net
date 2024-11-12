using System.ComponentModel.DataAnnotations;

namespace NETBACKING.CORE.APPLICATION.DTOs
{
    public class CreateUserDto
    {
        public string? Id { get; set; }
        
        public bool IsActive { get; set; }
    
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria")]
        public string Identification { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()._-])[A-Za-z\d!@#$%^&*()._-]{8,}$", 
            ErrorMessage = "La contraseña debe contener al menos 8 caracteres, incluyendo una letra mayúscula, una minúscula, un número y un carácter especial")]
        public string Password { get; set; }

        public string UserType { get; set; }
        
        [Range(500, 50000, ErrorMessage = "El monto inicial debe estar entre 500 y 50,000")]
        public decimal? InitialAmount { get; set; }

    }
}