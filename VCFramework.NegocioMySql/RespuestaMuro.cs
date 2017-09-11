using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;
using System.Data;
using System.Data.SqlClient;

namespace VCFramework.NegocioMySql
{
    public class RespuestaMuro
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

        public static VCFramework.Entidad.RespuestaMuro ObtenerRespuestaMuroPorId(int id)
        {
            VCFramework.Entidad.RespuestaMuro entidadDevolver = new Entidad.RespuestaMuro();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.RespuestaMuro>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.RespuestaMuro> lista2 = new List<VCFramework.Entidad.RespuestaMuro>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.RespuestaMuro>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
                entidadDevolver = lista2[0];
            return entidadDevolver;
        }

        public static List<VCFramework.Entidad.RespuestaMuro> ObtenerRespuestaMuroPorMuroId(int mroId)
        {

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "MRO_ID";
            filtro.Valor = mroId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.RespuestaMuro>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.RespuestaMuro> lista2 = new List<VCFramework.Entidad.RespuestaMuro>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.RespuestaMuro>().ToList();
            }

            return lista2;
        }
        public static int Modificar(VCFramework.Entidad.RespuestaMuro entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Update<Entidad.RespuestaMuro>(entidad, setCnsWebLun);
        }

        public static int Insertar(VCFramework.Entidad.RespuestaMuro entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Insertar<Entidad.RespuestaMuro>(entidad, setCnsWebLun);
        }
        public static int Eliminar(VCFramework.Entidad.RespuestaMuro entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Delete<Entidad.RespuestaMuro>(entidad, setCnsWebLun);
        }
    }
}
