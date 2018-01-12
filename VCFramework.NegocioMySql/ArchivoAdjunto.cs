using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class ArchivoAdjunto
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.ArchivoAdjunto> Listar()
        {
            List<VCFramework.Entidad.ArchivoAdjunto> retorno = new List<Entidad.ArchivoAdjunto>();

            VCFramework.Entidad.ArchivoAdjunto arch1 = new Entidad.ArchivoAdjunto();
            arch1.Id = 1;
            arch1.NombreArchivo = "BB__4.png";
            arch1.TipoPadre = 1; //novedades
            retorno.Add(arch1);

            VCFramework.Entidad.ArchivoAdjunto arch2 = new Entidad.ArchivoAdjunto();
            arch2.Id = 2;
            arch2.NombreArchivo = "cancha_1.jpg";
            arch2.TipoPadre = 1; //novedades
            retorno.Add(arch2);

            VCFramework.Entidad.ArchivoAdjunto arch3 = new Entidad.ArchivoAdjunto();
            arch1.Id = 3;
            arch1.NombreArchivo = "cancha_2.jpg";
            arch1.TipoPadre = 1; //novedades
            retorno.Add(arch3);


            return retorno;
        }
    }
}
