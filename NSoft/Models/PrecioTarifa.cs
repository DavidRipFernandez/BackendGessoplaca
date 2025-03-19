using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSoft.Models
{
    public class PrecioTarifa : AuditableEntity
    {
        public int MaterialId { get; set; }
        public int MarcaId { get; set; }
        public string ProveedorCifId { get; set; }

        [Required]
        public decimal Precio { get; set; }
        public bool Estado { get; set; } = true;

        [ForeignKey(nameof(MaterialId))]
        public Material Material { get; set; }

        //[ForeignKey(nameof(MarcaId))]
        //public Marca Marca { get; set; }

        [ForeignKey(nameof(ProveedorCifId) + "," + nameof(MarcaId))]
        public ProveedorMarca ProveedorMarca { get; set; }
    }
}
