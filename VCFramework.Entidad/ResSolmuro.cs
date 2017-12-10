using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class ResSolmuro
    {
        public int Id { get; set; }
        public int MroId { get; set; }
        public int InstId { get; set; }
        public int UsuId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int PrioridadId { get; set; }
        public string UrlImagen { get; set; }
        public int RolId { get; set; }
        public string Texto { get; set; }
        public int Eliminado { get; set; }
    }
}
