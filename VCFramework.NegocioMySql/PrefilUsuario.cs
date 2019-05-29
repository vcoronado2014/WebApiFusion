using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class PrefilUsuario
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static VCFramework.Entidad.MiPerfil BuscarPorId(int id)
        {
            VCFramework.Entidad.MiPerfil entidad = new Entidad.MiPerfil();
            List<VCFramework.Entidad.MiPerfil> archivos = new List<Entidad.MiPerfil>();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            filtros.Add(filtro);

            List<object> lista = fac.Leer<VCFramework.Entidad.MiPerfil>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.MiPerfil> lista2 = new List<VCFramework.Entidad.MiPerfil>();
            if (lista != null)
            {

                archivos = lista.Cast<VCFramework.Entidad.MiPerfil>().ToList();
            }
            if (archivos != null && archivos.Count == 1)
            {
                entidad = archivos[0];
            }

            return entidad;
        }

        public static VCFramework.Entidad.MiPerfil BuscarPorAusId(int ausId)
        {
            VCFramework.Entidad.MiPerfil entidad = new Entidad.MiPerfil();
            List<VCFramework.Entidad.MiPerfil> archivos = new List<Entidad.MiPerfil>();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "AUS_ID";
            filtro.Valor = ausId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            filtros.Add(filtro);

            List<object> lista = fac.Leer<VCFramework.Entidad.MiPerfil>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.MiPerfil> lista2 = new List<VCFramework.Entidad.MiPerfil>();
            if (lista != null)
            {

                archivos = lista.Cast<VCFramework.Entidad.MiPerfil>().ToList();
            }
            if (archivos != null && archivos.Count == 1)
            {
                entidad = archivos[0];
            }

            return entidad;
        }
        public static int Insertar(VCFramework.Entidad.MiPerfil entidad)
        {
            Factory fac = new Factory();
            return fac.Insertar<VCFramework.Entidad.MiPerfil>(entidad, setCnsWebLun);
        }

        public static int Eliminar(VCFramework.Entidad.MiPerfil entidad)
        {
            Factory fac = new Factory();
            return fac.Delete<VCFramework.Entidad.MiPerfil>(entidad, setCnsWebLun);
        }
        public static int Actualizar(VCFramework.Entidad.MiPerfil entidad)
        {
            Factory fac = new Factory();
            return fac.Update<VCFramework.Entidad.MiPerfil>(entidad, setCnsWebLun);
        }
    }
}
