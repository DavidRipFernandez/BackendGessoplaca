using NSoft.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSoft.DTOs
{
    public class PrecioTarifaDto
    {
        public int MaterialId { get; set; }
        public int MarcaId { get; set; }
        public required string ProveedorCifId { get; set; }

        public required decimal Precio { get; set; }

        public MaterialDto? Material { get; set; }
        public MarcaDto? Marca { get; set; }
        public ProveedorDto? Proveedor { get; set; }
    }
}
