using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.EntidadFuncional
{
    public class MuroFuncional : Entidad.Muro
    {
        public string NombreUsuario { get; set; }
        public string NombreRol { get; set; }
        public string FechaString { get; set; }
        public bool VisibleEliminar { get; set; }
        public List<EntidadFuncional.RespuestaMuroFuncional> RespuestaMuro { get; set; }
        public List<Entidad.ArchivoAdjunto> ArchivosAdjuntos { get; set; }
    }
}
