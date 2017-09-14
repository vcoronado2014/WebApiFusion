using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;
using System.Data;
using System.Data.SqlClient;

namespace VCFramework.NegocioMySql
{
    public class Mailing
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

        public static VCFramework.Entidad.Mailing ObtenerMailingPorId(int id)
        {
            VCFramework.Entidad.Mailing entidadDevolver = new Entidad.Mailing();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Mailing>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Mailing> lista2 = new List<VCFramework.Entidad.Mailing>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Mailing>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
                entidadDevolver = lista2[0];
            return entidadDevolver;
        }

        public static List<VCFramework.Entidad.Mailing> ObtenerMailingPorInstId(int instId)
        {

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Mailing>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Mailing> lista2 = new List<VCFramework.Entidad.Mailing>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Mailing>().ToList();
            }

            return lista2;
        }

        public static int Modificar(VCFramework.Entidad.Mailing entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Update<Entidad.Mailing>(entidad, setCnsWebLun);
        }

        public static int Insertar(VCFramework.Entidad.Mailing entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Insertar<Entidad.Mailing>(entidad, setCnsWebLun);
        }
        public static int Eliminar(VCFramework.Entidad.Mailing entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Delete<Entidad.Mailing>(entidad, setCnsWebLun);
        }
    }
}
