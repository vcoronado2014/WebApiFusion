using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class LogCarga
    {
        public int Id { get; set; }
        public int EccId { get; set; }
        public int NumeroFila { get; set; }
        public string NombreUsuario { get; set; }
        public string Detalle { get; set; }
        //tipo mensaje 0=error, 1=correcto
        public int TipoMensaje { get; set; }
    }
}
