using System.ComponentModel.DataAnnotations;

namespace Arbros.Shared.Models
{
    public class Usuarios
    {
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Error en el usuario,sin caracteres especiales")]
        public string User { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Key]
        [Required]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string Email { get; set; } = string.Empty;
    }
    public class Persona
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Se requiere el nombre")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Name { get; set; } = string.Empty;
    }
}

