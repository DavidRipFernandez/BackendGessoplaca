
namespace NSoft.DTOs
{
    public class PresupuestoDto
    {
        public int PresupuestoId { get; set; }

        public string Referencia { get; set; }

        public string NombreEmpresa { get; set; }

        public string CIF { get; set; }

        public string NombreContacto { get; set; }

        public string Direccion { get; set; }

        public string Poblacion { get; set; }

        public string Provincia { get; set; }

        public string CodigoPostal { get; set; }

        public int Telefono { get; set; }

        public string Email { get; set; }

        public string Estado { get; set; }

        public DateTime FechaLimitePresentacion { get; set; }

        public double TotalPresupuesto { get; set; }


        public ICollection<DescompuestoDto> Descompuestos { get; set; } = new List<DescompuestoDto>();
    }
}
