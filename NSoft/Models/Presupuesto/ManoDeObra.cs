using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSoft.Models.Presupuesto
{
    public class ManoDeObra:AuditableEntity
    {
        [Key]
        public int ManoObraId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public int UnidadesRealizar { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; } // Precio por unidad

        public int DescompuestoId { get; set; }
        [ForeignKey("DescompuestoId")]
        public virtual Descompuesto Descompuesto { get; set; }

        // Relación: Una ManoDeObra tiene un TipoManoObra
        public int TipoManoObraId { get; set; }
        [ForeignKey("TipoManoObraId")]
        public virtual TipoManoObra TipoManoObra { get; set; }
    }
}
