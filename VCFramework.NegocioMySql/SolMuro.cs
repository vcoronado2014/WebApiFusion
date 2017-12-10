using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;
using System.Data;
using System.Data.SqlClient;

namespace VCFramework.NegocioMySql
{
    public class SolMuro
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

        public static VCFramework.Entidad.SolMuro ObtenerMuroPorId(int id)
        {
            VCFramework.Entidad.SolMuro entidadDevolver = new Entidad.SolMuro();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.SolMuro>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.SolMuro> lista2 = new List<VCFramework.Entidad.SolMuro>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.SolMuro>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
                entidadDevolver = lista2[0];
            return entidadDevolver;
        }

        public static List<VCFramework.Entidad.SolMuro> ObtenerMuroPorInstId(int instId)
        {

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.SolMuro>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.SolMuro> lista2 = new List<VCFramework.Entidad.SolMuro>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.SolMuro>().ToList();
            }

            return lista2;
        }
        public static int Modificar(VCFramework.Entidad.SolMuro entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Update<Entidad.SolMuro>(entidad, setCnsWebLun);
        }

        public static int Insertar(VCFramework.Entidad.SolMuro entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Insertar<Entidad.SolMuro>(entidad, setCnsWebLun);
        }
        public static int Eliminar(VCFramework.Entidad.SolMuro entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Delete<Entidad.SolMuro>(entidad, setCnsWebLun);
        }
    }
}
