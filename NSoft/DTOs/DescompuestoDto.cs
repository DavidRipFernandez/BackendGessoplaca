using System.ComponentModel.DataAnnotations;

namespace NSoft.DTOs
{
    public class DescompuestoDto
    {
        public int DescompuestoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool IsPlantilla { get; set; }
        public string UnidadMedida { get; set; }
        public decimal Precio { get; set; }
        public decimal ManoObra { get; set; }
        public decimal Beneficio { get; set; }
        public decimal GastoAdministrativo { get; set; }
        public bool Estado { get; set; }
        public int? PresupuestoId { get; set; }

        public ICollection<DetalleDescompuestoDto> DetalleDescompuestos { get; set; } = new List<DetalleDescompuestoDto>();
        public ICollection<ManoDeObraDto> ManoDeObras { get; set; } = new List<ManoDeObraDto>();
    }

    // --- DTO para crear un Descompuesto ---
    public class DescompuestoCreacionDto
    {
        [Required]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool IsPlantilla { get; set; } = false;
        public string UnidadMedida { get; set; }
        public decimal Beneficio { get; set; }
        public int? PresupuestoId { get; set; }
    }

    // --- DTO para la actualización parcial ---
    public class DescompuestoEdicionParcialDto
    {
        [Required]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string UnidadMedida { get; set; }
        public decimal Beneficio { get; set; }
        public decimal ManoObra { get; set; }
        public decimal GastoAdministrativo { get; set; }
    }
}
