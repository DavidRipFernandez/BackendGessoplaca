using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSoft.Models
{
    public class ProveedorMarca : AuditableEntity
    {
        [Key]
        public int ProveedorMarcaId { get; set; }
        public string ProveedorCifId { get; set; }
        public Proveedor Proveedor { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }

    }
}
