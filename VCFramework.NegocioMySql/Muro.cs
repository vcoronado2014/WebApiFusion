using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;
using System.Data;
using System.Data.SqlClient;

namespace VCFramework.NegocioMySql
{
    public class Muro
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

        public static VCFramework.Entidad.Muro ObtenerMuroPorId(int id)
        {
            VCFramework.Entidad.Muro entidadDevolver = new Entidad.Muro();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Muro>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Muro> lista2 = new List<VCFramework.Entidad.Muro>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Muro>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
                entidadDevolver = lista2[0];
            return entidadDevolver;
        }

        public static List<VCFramework.Entidad.Muro> ObtenerMuroPorInstId(int instId)
        {

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Muro>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Muro> lista2 = new List<VCFramework.Entidad.Muro>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Muro>().ToList();
            }

            return lista2;
        }
        public static int Modificar(VCFramework.Entidad.Muro entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Update<Entidad.Muro>(entidad, setCnsWebLun);
        }

        public static int Insertar(VCFramework.Entidad.Muro entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Insertar<Entidad.Muro>(entidad, setCnsWebLun);
        }
        public static int Eliminar(VCFramework.Entidad.Muro entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Delete<Entidad.Muro>(entidad, setCnsWebLun);
        }

    }
}
