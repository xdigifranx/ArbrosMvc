using System.ComponentModel.DataAnnotations;

namespace Arbros.Shared.Models
{
    public class Persona
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Se requiere el nombre")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Name { get; set; } = string.Empty;
    }
}

