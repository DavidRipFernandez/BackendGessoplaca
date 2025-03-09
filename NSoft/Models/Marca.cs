using System.ComponentModel.DataAnnotations;

namespace NSoft.Models
{
    public class Marca : AuditableEntity
    {
        [Key]
        public int MarcaId { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }
        [MaxLength(100)]
        public string Descripcion { get; set; }
        public bool Estado { get; set; } = true;
        public ICollection<ProveedorMarca> ProveedoresMarcas { get; set; }
    }
}
