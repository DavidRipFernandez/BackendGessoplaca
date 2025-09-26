using NSoft.Models.Presupuesto;

namespace NSoft.DTOs
{
    // =======================================================
    // DTOs
    // =======================================================
    // ADDED
    using System.Collections.Generic;

    namespace NSoft.DTOs.Presupuesto
    {
        public class TipoManoObraDto
        {
            public int TipoManoObraId { get; set; }
            public string Nombre { get; set; } = default!;
            public bool Estado { get; set; }
        }


        public class TipoManoObraDetalleDto : TipoManoObraDto // ADDED
        {
            public List<ManoDeObraDto> ManoDeObras { get; set; } = new();
        }

        public class TipoManoObraCreateDto
        {
            public string Nombre { get; set; } = default!;
            public bool Estado { get; set; } = true;
        }

        public class TipoManoObraUpdateDto
        {
            public string Nombre { get; set; } = default!;
            public bool Estado { get; set; }
        }
    }
}
