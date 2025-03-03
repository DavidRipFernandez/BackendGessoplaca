using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSoft.Models
{
    public class ProveedorMarca : AuditableEntity
    {
        public string ProveedorCifId { get; set; }
        public int MarcaId { get; set; }


        [ForeignKey(nameof(ProveedorCifId))]
        public Proveedor Proveedor { get; set; }

        [ForeignKey(nameof(MarcaId))]
        public Marca Marca { get; set; }

    }
}
