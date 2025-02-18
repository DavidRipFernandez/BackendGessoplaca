namespace NSoft.Models
{
    public abstract class AuditableEntity
    {
        public DateTime? FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaModificacion {  get; set; } 
        public int? CreadoPor {  get; set; }    
        public int? ModificadoPor { get; set; }
    }
}
