using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.EntidadFuncional
{
    public class Inicio
    {
        public List<evento> Eventos { get; set; }
        public VCFramework.EntidadFuncional.proposalss Proyectos { get; set; }
        public VCFramework.EntidadFuncional.proposalss Votaciones { get; set; }
        public List<VCFramework.EntidadFuncional.UsuarioEnvoltorio> Usuarios { get; set; }
        public VCFramework.EntidadFuncional.proposalss Establecimientos { get; set; }
        public VCFramework.EntidadFuncional.proposalss Rendiciones { get; set; }
        public VCFramework.EntidadFuncional.proposalss Documentos { get; set; }

    }
    public class evento
    {

        public bool allDay { get; set; }

        public string content { get; set; }

        public string annoIni { get; set; }
        public string mesIni { get; set; }
        public string diaIni { get; set; }
        public string horaIni { get; set; }

        public string minutosIni { get; set; }

        public string annoTer { get; set; }
        public string mesTer { get; set; }
        public string diaTer { get; set; }
        public string horaTer { get; set; }
        public string minutosTer { get; set; }

        public int clientId { get; set; }
        public int id { get; set; }

    }
}
