using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class ArchivoAdjunto
    {

        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public string NombreCarpeta { get; set; }
        //corresponde por ejemplo a archivo adjunto de novedades
        //archivo adjunto de solicitudes
        //archivo adjunto de mercadito
        public int TipoPadre { get; set; }
        public int InstId { get; set; }
        public int ElementoId { get; set; }

    }
}
