namespace NSoft.DTOs
{
    public class MaterialConPrecioDto
    {
        public int MaterialId { get; set; }
        public string Nombre { get; set; } = null!;
        public string CodigoMaterial { get; set; } = null!;
        public string? SistemaMedicion { get; set; }
        public decimal Precio { get; set; }

        public MarcaDto Marca { get; set; } = null!;
        public ProveedorDto Proveedor { get; set; } = null!;
    }
}
