namespace NSoft.DTOs
{
    public class MarcaConPreciosDto
    {
        public int MarcaId { get; set; }
        public string NombreMarca { get; set; }
        public List<PrecioTarifaResumenDto> Materiales { get; set; } = new();
    }
}
