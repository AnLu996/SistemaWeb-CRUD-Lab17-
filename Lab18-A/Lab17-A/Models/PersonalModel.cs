using System.ComponentModel.DataAnnotations;

namespace Lab17_A.Models
{
    public class PersonalModel
    {
        public int IdPersonal { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido Paterno es obligatorio")]
        public string? ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "El campo Apellido Materno es obligatorio")]
        public string? ApellidoMaterno { get; set; }

        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Nacimiento es obligatorio")]
        public DateTime FechaNacimiento { get; set; }
        public int? Edad { get; set; }

        [Required(ErrorMessage = "El campo Genero es obligatorio")]
        public string? Genero { get; set; }

        [Required(ErrorMessage = "El campo Nacionalidad es obligatorio")]
        public string? Nacionalidad { get; set; }
        public DateTime FechaContratacion { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El campo Teléfono es obligatorio")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "El campo Sueldo es obligatorio")]
        public string? Sueldo { get; set; }

    }
}
