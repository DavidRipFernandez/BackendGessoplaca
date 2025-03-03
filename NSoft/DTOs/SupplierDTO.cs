namespace NSoft.DTOs
{
    public class SupplierDTO
    {
        public string ProveedorCifId { get; set; }
        public string Nombre { get; set; }
        public string DomicilioSocial { get; set; }
        public List<ContactoDTO> Contactos { get; set; }

    }

    public class ContactoDTO
    {
        public int ContactoId { get; set; }
        public string Nombre { get; set;}
        public string? Correo { get; set; }
        public string Telefono { get; set; }
        public string? Descripcion { get; set; }
    }
}
