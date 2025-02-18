namespace NSoft.Models
{
    public class RolModulo : AuditableEntity
    {
        public int RolId { get; set; }
        public Rol Rol { get; set; }
        public int ModuloId { get; set; }   
        public Modulo Modulo { get; set; }
        public int TipoPermisoId { get; set; }
        public TipoPermiso TipoPermiso { get; set; }
        public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();
    }
}
