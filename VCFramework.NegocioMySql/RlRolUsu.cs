using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class RlRolUsu
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static VCFramework.Entidad.UsuRol ObtenerPorId(int id)
        {
            VCFramework.Entidad.UsuRol retorno = new Entidad.UsuRol();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.UsuRol>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.UsuRol> lista2 = new List<VCFramework.Entidad.UsuRol>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.UsuRol>().ToList();
            }
            if (lista2 != null)
                retorno = lista2[0];

            return retorno;
        }
        public static List<VCFramework.Entidad.UsuRol> ObtenerPorInstId(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.UsuRol>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.UsuRol> lista2 = new List<VCFramework.Entidad.UsuRol>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.UsuRol>().ToList();
            }

            return lista2;
        }
        public static int Insertar(VCFramework.Entidad.UsuRol entidad)
        {
            Factory fac = new Factory();
            return fac.Insertar<VCFramework.Entidad.UsuRol>(entidad, setCnsWebLun);
        }

        public static int Eliminar(VCFramework.Entidad.UsuRol entidad)
        {
            Factory fac = new Factory();
            return fac.Delete<VCFramework.Entidad.UsuRol>(entidad, setCnsWebLun);
        }

    }
}
