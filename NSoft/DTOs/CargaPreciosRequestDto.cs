namespace NSoft.DTOs
{
    public class CargaPreciosRequestDto
    {
        public string? Empresa { get; set; }
        public List<CargaPrecioItemRequestDto> MaterialesProveedor { get; set; } = new();
    }
    public class CargaPrecioItemRequestDto
    {
        public string NombreMaterial { get; set; } = default!;
        public string? SistemaMedicion {  get; set; }
        public decimal Precio { get; set; }
        public string NombreMarca { get; set; }
        public int FilaExcel { get; set; }
    }
    public class CargaPrecioItemErrorDto
    {
        public int? MaterialId { get; set; }
        public string NombreMaterial { get; set; } = default!;
        public int? MarcaId { get; set; }
        public string NombreMarca { get; set; } = default!;
        public int Fila { get; set; }
        public string DetalleError { get; set; } = default!;
    }
    public class CargaPreciosResultadoErrorDto
    {
        public string ProveedorCifId { get; set; } = default!;
        public string? Empresa { get; set; }
        public List<CargaPrecioItemErrorDto> Materiales { get; set; } = new();
    }

}
