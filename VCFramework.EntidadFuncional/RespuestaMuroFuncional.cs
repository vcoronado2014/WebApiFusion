using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.EntidadFuncional
{
    public class RespuestaMuroFuncional : Entidad.RespuestaMuro
    {
        public string NombreUsuario { get; set; }
        public string NombreRol { get; set; }
        public string FechaString { get; set; }
    }
}
