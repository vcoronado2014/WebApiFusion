using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class MiPerfil : EntidadBase
    {
        public string Foto { get; set; }
        public string Iniciales { get; set; }
        public string Apodo { get; set; }
        public int AusId { get; set; }

    }
}
