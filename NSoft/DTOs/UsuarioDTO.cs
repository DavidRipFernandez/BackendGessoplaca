namespace NSoft.DTOs
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Constraseña { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
        public int RolId { get; set; }
        public string RolNombre { get; set; }
    }
}
