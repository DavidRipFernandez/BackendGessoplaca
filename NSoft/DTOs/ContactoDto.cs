namespace NSoft.DTOs
{
    public class ContactoDto
    {
        public int? ContactoId { get; set; }
        public required string Nombre { get; set; }
        public string? Correo { get; set; }
        public required string Telefono { get; set; }
        public string? Descripcion { get; set; }
        public bool? Estado { get; set; } = true; 
        public string? ProveedorCif { get; set; }

        public ProveedorDto? Proveedor { get; set; }

    }
}
