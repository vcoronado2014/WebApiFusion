using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.EntidadFuncional
{
    public class ReportesFuncional
    {
        public int InstId { get; set; }
        public string Institucion { get; set; }
        public string Fechas { get; set; }
        public string Nombre { get; set; }
        public string Lista { get; set; }
        public int VotosSi { get; set; }
        public int VotosNo { get; set; }
        public string Tipo { get; set; }
    }
}
