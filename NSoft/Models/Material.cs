using System.ComponentModel.DataAnnotations;

namespace NSoft.Models
{
    public class Material : AuditableEntity
    {
        [Key]
        public int MaterialId { get; set; }
        [Required, MaxLength(100)]
        public string CodigoMaterial { get; set; }
        [Required, MaxLength(100)]
        public string Nombre { get; set; } 
        [MaxLength(100)]
        public string SistemaMedicion { get; set; }
        public bool Estado { get; set; } = true;
        // 🔥 Relación con Categoría de Material
        public int CategoriaId { get; set; }
        public CategoriaMaterial CategoriasMateriales { get; set; }
    }
}
