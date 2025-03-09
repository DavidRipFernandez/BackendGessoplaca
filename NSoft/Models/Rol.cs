using System.ComponentModel.DataAnnotations;

namespace NSoft.Models
{
    public class Rol : AuditableEntity
    {
        [Key]
        public int RolId { get; set; }
        [Required, MaxLength(50)]
        public string NombreRol { get; set; }
        [Required, MaxLength(100)]
        public string Descripcion { get; set; }
        public bool Estado { get; set; } = true;
        public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();
        // Un rol puede tener muchos usuarios 
        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<RolModulo> RolesModulos { get; set; }
    }
}
