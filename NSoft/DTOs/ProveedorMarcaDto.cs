using NSoft.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSoft.DTOs
{
    public class ProveedorMarcaDto
    {

        public required string ProveedorCifId { get; set; }
        public int MarcaId { get; set; }
        public bool Estado { get; set; } = true;
        public ProveedorDto? Proveedor { get; set; }
        public MarcaDto? Marca { get; set; }
        public ICollection<PrecioTarifaDto> PrecioTarifa { get; set; } = new List<PrecioTarifaDto>();

    }
}
