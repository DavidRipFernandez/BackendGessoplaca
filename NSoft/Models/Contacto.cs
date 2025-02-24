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

        [ForeignKey(nameof(Proveedor))]
        public string ProveedorCifId { get; set; }

        //[JsonIgnore]
        public virtual Proveedor? Proveedor { get; set; }
    }
}
