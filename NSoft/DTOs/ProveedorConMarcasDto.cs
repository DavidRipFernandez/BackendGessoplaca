namespace NSoft.DTOs
{
    public class ProveedorConMarcasDto
    {
        public string ProveedorCifId { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? DomicilioSocial { get; set; }
        public List<MarcaDto> Marcas { get; set; } = new();
    }
}
