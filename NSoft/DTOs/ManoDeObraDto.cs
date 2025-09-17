using NSoft.DTOs.NSoft.DTOs.Presupuesto;

namespace NSoft.DTOs
{
    public class ManoDeObraDto
    {
        public int ManoDeObraId { get; set; }
        public string Nombre { get; set; }
        public int UnidadesRealizar { get; set; }
        public decimal Precio { get; set; }

        public int TipoManoObraId { get; set; }

        public TipoManoObraDto? TipoManoObra { get; set; }// Opcional, pero útil
    }
    

    public class ManoDeObraCreacionDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int UnidadesRealizar { get; set; }
        public decimal Precio { get; set; }
        public int DescompuestoId { get; set; }
        public int TipoManoObraId { get; set; }
    }

    public class ManoDeObraEdicionDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int UnidadesRealizar { get; set; }
        public decimal Precio { get; set; }
        public int DescompuestoId { get; set; }
        public int TipoManoObraId { get; set; }
    }
}
