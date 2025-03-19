using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSoft.Models
{
    public class Material : AuditableEntity
    {
        [Key]
        public int MaterialId { get; set; }
        [Required, MaxLength(100)]
        public required string CodigoMaterial { get; set; }
        [Required, MaxLength(100)]
        public required string Nombre { get; set; } 
        [MaxLength(100)]
        public string? SistemaMedicion { get; set; }
        public bool Estado { get; set; } = true;
        [Required]
        [ForeignKey(nameof(CategoriaId))]
        public int CategoriaId { get; set; }
        public CategoriaMaterial CategoriasMaterial { get; set; }

        public ICollection<PrecioTarifa> precioTarifas { get; set; } = new List<PrecioTarifa>();
    }
}
