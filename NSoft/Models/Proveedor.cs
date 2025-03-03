using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace NSoft.Models
{
    public class Proveedor : AuditableEntity
    {

        [Key]
        [MaxLength(50)]
        public required string ProveedorCifId { get; set; }

        [Required, MaxLength(100)]
        public required string Nombre { get; set; }

        [MaxLength(100)]
        public required string DomicilioSocial { get; set; }

        public ICollection<Contacto> Contactos { get; set; } = new List<Contacto>();
        public ICollection<ProveedorMarca> ProveedoresMarcas { get; set; } = new List<ProveedorMarca>();
    }
}
