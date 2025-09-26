using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSoft.Models.Presupuesto
{
    public class Descompuesto:AuditableEntity
    {
        [Key]
        public int DescompuestoId { get; set; }

        [Required]
        [StringLength(150)]
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [Required]
        public bool IsPlantilla { get; set; } = false;

        [StringLength(50)]
        public string UnidadMedida { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cantidad { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ManoObra { get; set; } // Coste total de mano de obra para este descompuesto

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Beneficio { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal GastoAdministrativo { get; set; }

        public bool Estado {  get; set; } = true;

        // Relación: Un Descompuesto pertenece a un Presupuesto
        public int? PresupuestoId { get; set; }
        [ForeignKey("PresupuestoId")]
        public virtual Presupuesto Presupuesto { get; set; }

        // Relación: Un Descompuesto tiene muchos Detalles
        public virtual ICollection<DetalleDescompuesto> DetalleDescompuestos { get; set; } = new List<DetalleDescompuesto>();

        // Relación: Un Descompuesto tiene muchos registros de Mano de Obra
        public virtual ICollection<ManoDeObra> ManoDeObras { get; set; } = new List<ManoDeObra>();
    }
}
