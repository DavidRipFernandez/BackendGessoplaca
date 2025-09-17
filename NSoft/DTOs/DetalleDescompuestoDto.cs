namespace NSoft.DTOs
{
    public class DetalleDescompuestoDto
    {
        public int DetalleDescompuestoId { get; set; }
        public string NombreMaterial { get; set; }
        public string Proveedor { get; set; }
        public decimal Unidades { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public bool Estado { get; set; }
    }
}
