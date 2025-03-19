using NSoft.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSoft.DTOs
{
    public class PrecioTarifaDto
    {
        public int MaterialId { get; set; }
        public int MarcaId { get; set; }
        public string ProveedorCifId { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; } = true;
    }
}
