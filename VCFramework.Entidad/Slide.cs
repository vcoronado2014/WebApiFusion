using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.Entidad
{
    public class Slide
    {
        public int Id { get; set; }
        public int InstId { get; set; }
        public int EsVisible { get; set; }
        public string UrlImagen { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int Eliminado { get; set; }
    }
}
