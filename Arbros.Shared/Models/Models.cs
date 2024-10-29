﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Arbros.Shared.Models
{
    public class Paises
    {
        public int Id { get; set; }
        public string Pais { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Persona>? Personas { get; set; }
	}
    public class Tiempo
    {
        public int Id { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? Tareas { get; set; }
    }
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

        [Required(ErrorMessage = "El campo Pais es obligatorio.")]
        public int PaisId { get; set; }

        public Paises? Pais { get; set; } // Asegúrate de que esta propiedad esté correctamente configurada
    }

}

