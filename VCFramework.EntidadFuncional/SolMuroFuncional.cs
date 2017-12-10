using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.EntidadFuncional
{
    public class SolMuroFuncional : Entidad.SolMuro
    {
        public string NombreUsuario { get; set; }
        public string NombreRol { get; set; }
        public string FechaString { get; set; }
        public bool VisibleEliminar { get; set; }
        public List<EntidadFuncional.ResSolmuroFuncional> RespuestaMuro { get; set; }
    }
}
