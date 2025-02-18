using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSoft.Models
{
    public class PrecioTarifa : AuditableEntity
    {
        [Key]
        public int PrecioTarifaId { get; set; }

        [Required]
        public decimal Precio { get; set; }

        // 🔥 Clave foránea a Material
        public int MaterialId { get; set; }
        public Material Materiales { get; set; }

        // 🔥 Clave foránea a ProveedorMarca
        public int ProveedorMarcaId { get; set; }
        public ProveedorMarca ProveedoresMarcas { get; set; }
    }
}
