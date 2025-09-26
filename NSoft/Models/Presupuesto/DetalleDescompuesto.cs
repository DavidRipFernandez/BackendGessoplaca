using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSoft.Models.Presupuesto
{
    public class DetalleDescompuesto:AuditableEntity
    {
        [Key]
        public int DetalleDescompuestoId { get; set; }

        [Required]
        [StringLength(150)]
        public string NombreMaterial { get; set; }

        [StringLength(100)]
        public string Proveedor { get; set; }

        [StringLength(100)]
        public string Marca { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Unidades { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Descuento { get; set; }

        public bool Estado {  get; set; } = true;

        // Relación: Un Detalle pertenece a un Descompuesto
        public int DescompuestoId { get; set; }
        [ForeignKey("DescompuestoId")]
        public virtual Descompuesto Descompuesto { get; set; }
    }
}
