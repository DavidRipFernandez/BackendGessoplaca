using System.ComponentModel.DataAnnotations;

namespace NSoft.Models
{
    public class CategoriaMaterial : AuditableEntity
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required, MaxLength(100)]
        public required string Nombre { get; set; }
        [MaxLength(100)]
        public string? Descripcion { get; set; }
        public bool Estado { get; set; } = true;
        public ICollection<Material> Materiales { get; set; } = new List<Material>();
    }
}
