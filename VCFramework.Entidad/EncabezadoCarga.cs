using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class EncabezadoCarga
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime Fecha { get; set; }
        public int FilasError { get; set; }
        public int FilasCorrectas { get; set; }
        public int InstId { get; set; }
        public string Resumen { get; set; }
    }
}
