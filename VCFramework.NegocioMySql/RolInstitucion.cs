using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class RolInstitucion
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];

        public static List<VCFramework.Entidad.RolInstitucion> ObtenerRolesPorInstId(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.RolInstitucion>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.RolInstitucion> lista2 = new List<VCFramework.Entidad.RolInstitucion>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.RolInstitucion>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);

            return lista2;
        }
        public static VCFramework.Entidad.RolInstitucion ObtenerRolPorId(int id)
        {
            VCFramework.Entidad.RolInstitucion retorno = new Entidad.RolInstitucion();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.RolInstitucion>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.RolInstitucion> lista2 = new List<VCFramework.Entidad.RolInstitucion>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.RolInstitucion>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
                retorno = lista2[0];

            return retorno;
        }
        public static VCFramework.Entidad.RolInstitucion ObtenerRolPorPadreId(int id, int instId)
        {
            VCFramework.Entidad.RolInstitucion retorno = new Entidad.RolInstitucion();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID_ORIGINAL";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            FiltroGenerico filtro1 = new FiltroGenerico();
            filtro1.Campo = "INST_ID";
            filtro1.Valor = instId.ToString();
            filtro1.TipoDato = TipoDatoGeneral.Entero;

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);
            filtros.Add(filtro1);

            List<object> lista = fac.Leer<VCFramework.Entidad.RolInstitucion>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.RolInstitucion> lista2 = new List<VCFramework.Entidad.RolInstitucion>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.RolInstitucion>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
                retorno = lista2[0];

            return retorno;
        }
        public static int Modificar(VCFramework.Entidad.RolInstitucion rol)
        {

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();

            return fac.Update<VCFramework.Entidad.RolInstitucion>(rol, setCnsWebLun);
        }
        public static int Insertar(VCFramework.Entidad.RolInstitucion rol)
        {

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();

            return fac.Insertar<VCFramework.Entidad.RolInstitucion>(rol, setCnsWebLun);
        }
        public static int Eliminar(VCFramework.Entidad.RolInstitucion rol)
        {

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();

            return fac.Delete<VCFramework.Entidad.RolInstitucion>(rol, setCnsWebLun);
        }

    }
}
