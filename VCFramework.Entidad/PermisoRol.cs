using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class PermisoRol
    {
        public int Id { get; set; }
        public int InstId { get; set; }
        public int RolId { get; set; }
        public int CreaUsuario { get; set; }
        public int ModificaUsuario { get; set; }
        public int EliminaUsuario { get; set; }
        public int VerUsuario { get; set; }
        public int CreaInstitucion { get; set; }
        public int ModificaInstitucion { get; set; }
        public int EliminaInstitucion { get; set; }
        public int VerInstitucion { get; set; }
        public int CreaDocumento { get; set; }
        public int EliminaDocumento { get; set; }
        public int VerDocumento { get; set; }
        public int CreaCalendario { get; set; }
        public int ModificaCalendario { get; set; }
        public int EliminaCalendario { get; set; }
        public int VerCalendario { get; set; }
        public int CreaTricel { get; set; }
        public int ModificaTricel { get; set; }
        public int EliminaTricel { get; set; }
        public int VerTricel { get; set; }
        public int CreaProyecto { get; set; }
        public int ModificaProyecto { get; set; }
        public int EliminaProyecto { get; set; }
        public int VerProyecto { get; set; }
        public int CreaRendicion { get; set; }
        public int ModificaRendicion { get; set; }
        public int EliminaRendicion { get; set; }
        public int VerRendicion { get; set; }
        public int CreaRol { get; set; }
        public int ModificaRol { get; set; }
        public int EliminaRol { get; set; }
        public int VerRol { get; set; }
        public int CreaMuro { get; set; }
        public int ModificaMuro { get; set; }
        public int EliminaMuro { get; set; }
        public int VerMuro { get; set; }
        public int PuedeVotarProyecto { get; set; }
        public int PuedeVotarTricel { get; set; }
        public int VerMailing { get; set; }
        public int CreaMailing { get; set; }
        public int VerReportes { get; set; }
        public int VerReporteAsistencia { get; set; }


    }
}
