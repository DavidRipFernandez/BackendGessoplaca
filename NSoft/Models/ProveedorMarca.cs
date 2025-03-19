using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSoft.Models
{
    public class ProveedorMarca : AuditableEntity
    {
        [Key, Column(Order = 0)]
        public required string ProveedorCifId { get; set; }
        [Key, Column(Order = 1)]
        public int MarcaId { get; set; }
        public bool Estado { get; set; } = true;

        [ForeignKey(nameof(ProveedorCifId))]
        public Proveedor Proveedor { get; set; } 

        [ForeignKey(nameof(MarcaId))]
        public Marca Marca { get; set; }

        public ICollection<PrecioTarifa> PrecioTarifa { get; set; } = new List<PrecioTarifa>();
    }
}
