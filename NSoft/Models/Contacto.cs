using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSoft.Models
{
    public class Contacto : AuditableEntity
    {
        [Key]
        public int ContactoId { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Correo { get; set; }

        [MaxLength(50)]
        public string Telefono { get; set; }

        [MaxLength(100)]
        public string Descripcion { get; set; }
        // 🔥 Clave foránea a Proveedor
        public string ProveedorCifId { get; set; }
        public Proveedor Proveedores { get; set; }
    }
}
