namespace NSoft.DTOs
{
    public class PermisoDto
    {
        public string Modulo { get; set; }
        public string Permiso { get; set; }

        public PermisoDto() { }
        public PermisoDto(string modulo, string permiso) 
        {
            Modulo = modulo;
            Permiso = permiso;
        }
    }
}
