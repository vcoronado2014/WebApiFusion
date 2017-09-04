using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class RolInstitucion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int InstId { get; set; }
        public int IdOriginal { get; set; }
        public int Eliminado { get; set; }
    }
}
