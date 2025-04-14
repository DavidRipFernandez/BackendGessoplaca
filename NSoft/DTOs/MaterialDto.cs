using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSoft.DTOs
{
    public class MaterialDto
    {
        public int MaterialId { get; set; }
        public required string CodigoMaterial { get; set; }
        public required string Nombre { get; set; }
        public string? SistemaMedicion { get; set; }
        public bool Estado { get; set; } = true;
        public int CategoriaId { get; set; }
        public CategoriaMaterialDto? Categoria { get; set; }
    }
}
