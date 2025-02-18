using System.ComponentModel.DataAnnotations;

namespace NSoft.Models
{
    public class Usuario : AuditableEntity
    {
        [Key]
        public int UsuarioId { get; set; }
        [Required, MaxLength(100)]
        public string Nombre { get; set; }
        [Required, MaxLength(150)]
        public string Correo { get; set; }
        [Required]
        public string ContraseñaHash { get; set; }
        public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();
        public bool Estado { get; set; } = true;
        public DateTime FechaCreacion { get; set; }
        public int? ModificadoPor { get; set; }

        //relacion con roles
        public int RolId { get; set; } //clave foranea
        public Rol Rol { get; set; }    //relacion con la entidad Rol
     }
}
