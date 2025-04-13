namespace NSoft.DTOs
{
    public class PreciosProveedorDto
    {
        public string ProveedorCifId { get; set; }
        public string NombreProveedor { get; set; }
        public List<PrecioTarifaResumenDto> Materiales { get; set; } = new();
    }
}
