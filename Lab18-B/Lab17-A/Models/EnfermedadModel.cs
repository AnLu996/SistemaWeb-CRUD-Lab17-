using System.ComponentModel.DataAnnotations;

namespace Lab17_A.Models
{
    public class EnfermedadModel
    {
        public int IdEnfermedad { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo Tasa de Mortalidad es obligatorio")]
        public string? TasaMortalidad { get; set; }

        [Required(ErrorMessage = "El campo Sintoma es obligatorio")]
        public string? Sintoma { get; set; }

        [Required(ErrorMessage = "El campo Medicamento es obligatorio")]
        public string? Medicamento { get; set; } = "No tiene";

    }
}
