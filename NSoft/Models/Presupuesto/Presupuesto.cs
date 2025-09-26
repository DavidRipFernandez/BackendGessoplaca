using System.ComponentModel.DataAnnotations;

namespace NSoft.Models.Presupuesto
{
    public class Presupuesto : AuditableEntity
    {
        [Key]
        public int PresupuestoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Referencia { get; set; }

        [StringLength(200)]
        public string NombreEmpresa { get; set; }

        [StringLength(50)]
        public string CIF { get; set; }

        [StringLength(150)]
        public string NombreContacto { get; set; }

        [StringLength(255)]
        public string Direccion { get; set; }

        [StringLength(100)]
        public string Poblacion { get; set; }

        [StringLength(100)]
        public string Provincia { get; set; }

        [StringLength(10)]
        public string CodigoPostal { get; set; }

        public int Telefono { get; set; }

        public string Estado { get; set; } = "Pendiente";

        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        public DateTime FechaLimitePresentacion { get; set; }

        public double TotalPresupuesto { get; set; }

        // Relación: Un Presupuesto tiene muchos Descompuestos
        public virtual ICollection<Descompuesto> Descompuestos { get; set; } = new List<Descompuesto>();
    }
}
