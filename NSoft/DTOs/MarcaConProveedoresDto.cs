namespace NSoft.DTOs
{
    public class MarcaConProveedoresDto
    {
        public int MarcaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public List<ProveedorDto> Proveedores { get; set; } = new();
    }
}
