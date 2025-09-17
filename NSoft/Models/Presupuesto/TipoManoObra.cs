using System.ComponentModel.DataAnnotations;

namespace NSoft.Models.Presupuesto
{
    public class TipoManoObra:AuditableEntity
    {
        [Key]
        public int TipoManoObraId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        public bool Estado { get; set; }

        // Relación inversa (opcional, pero buena práctica)
        public virtual ICollection<ManoDeObra> ManoDeObras { get; set; } = new List<ManoDeObra>();
    }
}
