namespace NSoft.DTOs
{
    public class DetalleDescompuestoDto
    {
        public int DetalleDescompuestoId { get; set; }
        public string NombreMaterial { get; set; }
        public string? Proveedor { get; set; }
        public string? Marca { get; set; }
        public decimal Unidades { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public bool Estado { get; set; }
        public int DescompuestoId { get; set; }
    }

    public class DetalleDescompuestoCreacionDto
    {
        public string NombreMaterial { get; set; }
        public string? Proveedor { get; set; }
        public string? Marca { get; set; }
        public decimal Unidades { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public int DescompuestoId { get; set; }
    }

    // DTO para editar un DetalleDescompuesto existente.
    public class DetalleDescompuestoEdicionDto
    {
        public int DetalleDescompuestoId { get; set; }
        public string NombreMaterial { get; set; }
        public string? Proveedor { get; set; }
        public string? Marca { get; set; }
        public decimal Unidades { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public bool Estado { get; set; }
        public int DescompuestoId { get; set; }
    }
}
