namespace NSoft.DTOs
{
    public class PrecioTarifaResumenDto
    {
        public int MaterialId { get; set; }
        public string NombreMaterial { get; set; } = string.Empty;
        public string CodigoMaterial { get; set; } = string.Empty;
        public string? SistemaMedicion { get; set; }

        public decimal Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public int MarcaId { get; set; }
        public string NombreMarca { get; set; } = string.Empty;

        public string ProveedorCifId { get; set; } = string.Empty;
        public string NombreProveedor { get; set; } = string.Empty;
    }
}
