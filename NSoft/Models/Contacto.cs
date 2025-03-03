using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NSoft.Models
{
    public class Contacto : AuditableEntity
    {
        [Key]
        public int ContactoId { get; set; }

        [Required, MaxLength(100)]
        public required string Nombre { get; set; }

        [MaxLength(100)]
        public string? Correo { get; set; }

        [MaxLength(50)]
        public required string Telefono { get; set; }

        [MaxLength(100)]
        public string? Descripcion { get; set; }

        [Required]
        [MaxLength(50)]
        [ForeignKey(nameof(Proveedor))]
        public required string ProveedorCifId { get; set; }

        public virtual Proveedor? Proveedor { get; set; }
    }
}
