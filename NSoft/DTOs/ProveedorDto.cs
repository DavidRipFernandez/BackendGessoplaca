namespace NSoft.DTOs
{
    public class ProveedorDto
    {
        public string ProveedorCifId { get; set; }
        public string Nombre { get; set; }
        public string DomicilioSocial { get; set; }
        public List<ContactoDto>? Contactos { get; set; } = new List<ContactoDto>();
        public List<MarcaDto>? Marcas { get; set; } = new List<MarcaDto>();
        public ICollection<ProveedorMarcaDto>? ProveedoresMarcas { get; set; } = new List<ProveedorMarcaDto>();

    }
}
