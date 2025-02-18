using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace NSoft.Models
{
    public class Proveedor : AuditableEntity
    {
        [Key]
        [MaxLength(50)]
        public string ProveedorCifId { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string DomicilioSocial { get; set; }
        // Relación con Contactos y ProveedorMarcas
        public ICollection<Contacto> Contactos { get; set; }
        public ICollection<ProveedorMarca> ProveedoresMarcas { get; set; }
    }
}
