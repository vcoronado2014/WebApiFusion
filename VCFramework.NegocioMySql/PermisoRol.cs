using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;
using System.Data;
using System.Data.SqlClient;

namespace VCFramework.NegocioMySql
{
    public class PermisoRol
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

        public static VCFramework.Entidad.PermisoRol ObtenerPermisoRolPorId(int id)
        {
            VCFramework.Entidad.PermisoRol entidadDevolver = new Entidad.PermisoRol();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.PermisoRol>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.PermisoRol> lista2 = new List<VCFramework.Entidad.PermisoRol>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.PermisoRol>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
                entidadDevolver = lista2[0];
            return entidadDevolver;
        }
        public static List<VCFramework.Entidad.PermisoRol> ObtenerPermisoRolPorInstId(int instId)
        {

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.PermisoRol>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.PermisoRol> lista2 = new List<VCFramework.Entidad.PermisoRol>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.PermisoRol>().ToList();
            }

            return lista2;
        }

        public static List<VCFramework.Entidad.PermisoRol> ObtenerPermisoRolPorRolId(int rolId, int instId)
        {

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            FiltroGenerico filtro1 = new FiltroGenerico();
            filtro1.Campo = "ROL_ID";
            filtro1.Valor = rolId.ToString();
            filtro1.TipoDato = TipoDatoGeneral.Entero;

            filtros.Add(filtro1);
            filtros.Add(filtro);

            List<object> lista = fac.Leer<VCFramework.Entidad.PermisoRol>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.PermisoRol> lista2 = new List<VCFramework.Entidad.PermisoRol>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.PermisoRol>().ToList();
            }

            return lista2;
        }

        public static int Modificar(VCFramework.Entidad.PermisoRol entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Update<Entidad.PermisoRol>(entidad, setCnsWebLun);
        }

        public static int Insertar(VCFramework.Entidad.PermisoRol entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Insertar<Entidad.PermisoRol>(entidad, setCnsWebLun);
        }
        public static int Eliminar(VCFramework.Entidad.PermisoRol entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Delete<Entidad.PermisoRol>(entidad, setCnsWebLun);
        }

    }
}
