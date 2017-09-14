using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class Mailing
    {
        public int Id { get; set; }
        public int InstId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuId { get; set; }
        public int CreaUsuario { get; set; }
        public int ModificaUsuario { get; set; }
        public int EliminaUsuario { get; set; }
        public int CreaInstitucion { get; set; }
        public int ModificaInstitucion { get; set; }
        public int EliminaInstitucion { get; set; }
        public int CreaDocumento { get; set; }
        public int EliminaDocumento { get; set; }
        public int CreaCalendario { get; set; }
        public int ModificaCalendario { get; set; }
        public int EliminaCalendario { get; set; }
        public int CreaTricel { get; set; }
        public int ModificaTricel { get; set; }
        public int EliminaTricel { get; set; }
        public int CreaProyecto { get; set; }
        public int ModificaProyecto { get; set; }
        public int EliminaProyecto { get; set; }
        public int CreaRendicion { get; set; }
        public int ModificaRendicion { get; set; }
        public int EliminaRendicion { get; set; }
        public int CreaRol { get; set; }
        public int ModificaRol { get; set; }
        public int EliminaRol { get; set; }
        public int CreaMuro { get; set; }
        public int ModificaMuro { get; set; }
        public int EliminaMuro { get; set; }
        public int Eliminado { get; set; }
    }
}
