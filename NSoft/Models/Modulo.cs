using System.ComponentModel.DataAnnotations;

namespace NSoft.Models
{
    public class Modulo : AuditableEntity
    {
        [Key]
        public int ModuloId { get; set; }

        [Required, MaxLength(50)]
        public string ModuloCodigo { get; set; }

        [Required, MaxLength(50)]
        public string NombreModulo { get; set; }

        public string Descripcion { get; set; }

        //public Guid SecurityStamp { get; set; } = Guid.NewGuid();
        public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();
        public ICollection<RolModulo> RolesModulos { get; set; }
    }
}
