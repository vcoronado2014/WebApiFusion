using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;
using System.Data;
using System.Data.SqlClient;

namespace VCFramework.NegocioMySql
{
    public class ResSolmuro
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

        public static VCFramework.Entidad.ResSolmuro ObtenerRespuestaMuroPorId(int id)
        {
            VCFramework.Entidad.ResSolmuro entidadDevolver = new Entidad.ResSolmuro();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.ResSolmuro>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.ResSolmuro> lista2 = new List<VCFramework.Entidad.ResSolmuro>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.ResSolmuro>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
                entidadDevolver = lista2[0];
            return entidadDevolver;
        }

        public static List<VCFramework.Entidad.ResSolmuro> ObtenerRespuestaMuroPorMuroId(int mroId)
        {

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "MRO_ID";
            filtro.Valor = mroId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.ResSolmuro>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.ResSolmuro> lista2 = new List<VCFramework.Entidad.ResSolmuro>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.ResSolmuro>().ToList();
            }

            return lista2;
        }
        public static int Modificar(VCFramework.Entidad.ResSolmuro entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Update<Entidad.ResSolmuro>(entidad, setCnsWebLun);
        }

        public static int Insertar(VCFramework.Entidad.ResSolmuro entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Insertar<Entidad.ResSolmuro>(entidad, setCnsWebLun);
        }
        public static int Eliminar(VCFramework.Entidad.ResSolmuro entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Delete<Entidad.ResSolmuro>(entidad, setCnsWebLun);
        }
    }
}
