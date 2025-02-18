using System.ComponentModel.DataAnnotations;

namespace NSoft.Models
{
    public class TipoPermiso : AuditableEntity
    {
        [Key]
        public int TipoPermisoId {  get; set; }
        [Required, MaxLength(10)]
        public string NombrePermiso { get; set; }
        public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();

        //Relacion de uno a muchos con RolesModulos
        public ICollection<RolModulo> RolModulos { get; set; }

    }
}
