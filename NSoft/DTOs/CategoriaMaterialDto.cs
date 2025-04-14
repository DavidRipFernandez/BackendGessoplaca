namespace NSoft.DTOs
{
    public class CategoriaMaterialDto
    {
        public int CategoriaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool? Estado { get; set; }

        public List<MaterialDto>? Materiales { get; set; }
    }
}
