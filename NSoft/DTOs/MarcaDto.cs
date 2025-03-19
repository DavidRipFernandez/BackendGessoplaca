namespace NSoft.DTOs
{
    public class MarcaDto
    {

        public int MarcaId { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; } = true;
        public ICollection<ProveedorDto> ProveedoresMarcas { get; set; } = new List<ProveedorDto>();

    }
}
