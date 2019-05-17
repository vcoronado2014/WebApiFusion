using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class TokenUsuario
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];

        public static int Insertar(VCFramework.Entidad.TokenUsuario aus)
        {
            aus.Nuevo = true;
            aus.Borrado = false;
            aus.Modificado = false;

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();

            return fac.Insertar<VCFramework.Entidad.TokenUsuario>(aus, setCnsWebLun);
        }
        public static int Modificar(VCFramework.Entidad.TokenUsuario aus)
        {
            aus.Nuevo = false;
            aus.Borrado = false;
            aus.Modificado = true;

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();

            return fac.Update<VCFramework.Entidad.TokenUsuario>(aus, setCnsWebLun);
        }
        public static List<VCFramework.Entidad.TokenUsuario> Listar()
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();

            List<object> lista = fac.Leer<VCFramework.Entidad.TokenUsuario>(setCnsWebLun);
            List<VCFramework.Entidad.TokenUsuario> lista2 = new List<VCFramework.Entidad.TokenUsuario>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.TokenUsuario>().ToList();
            }


            return lista2;
        }
        public static VCFramework.Entidad.TokenUsuario ObtenerPorToken(string token)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "TOKEN";
            filtro.TipoDato = TipoDatoGeneral.Varchar;
            filtro.Valor = token;
            VCFramework.Entidad.TokenUsuario retorno = new Entidad.TokenUsuario();

            List<object> lista = fac.Leer<VCFramework.Entidad.TokenUsuario>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.TokenUsuario> lista2 = new List<VCFramework.Entidad.TokenUsuario>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.TokenUsuario>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
            {
                retorno = lista2[0]; 
            }

            return retorno;
        }
        public static VCFramework.Entidad.TokenUsuario ObtenerPorToken(string token, int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "TOKEN";
            filtro.TipoDato = TipoDatoGeneral.Varchar;
            filtro.Valor = token;

            FiltroGenerico filtro1 = new FiltroGenerico();
            filtro1.Campo = "INST_ID";
            filtro1.TipoDato = TipoDatoGeneral.Entero;
            filtro1.Valor = instId.ToString();

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);
            filtros.Add(filtro1);
            VCFramework.Entidad.TokenUsuario retorno = new Entidad.TokenUsuario();

            List<object> lista = fac.Leer<VCFramework.Entidad.TokenUsuario>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.TokenUsuario> lista2 = new List<VCFramework.Entidad.TokenUsuario>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.TokenUsuario>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
            {
                retorno = lista2[0];
            }

            return retorno;
        }
        public static List<VCFramework.Entidad.TokenUsuario> Listar(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.TipoDato = TipoDatoGeneral.Entero;
            filtro.Valor = instId.ToString();

            List<object> lista = fac.Leer<VCFramework.Entidad.TokenUsuario>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.TokenUsuario> lista2 = new List<VCFramework.Entidad.TokenUsuario>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.TokenUsuario>().ToList();
            }


            return lista2;
        }

        public static VCFramework.Entidad.TokenUsuario ObtenerPorToken(int usuId, int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "USU_ID";
            filtro.TipoDato = TipoDatoGeneral.Entero;
            filtro.Valor = usuId.ToString();

            FiltroGenerico filtro1 = new FiltroGenerico();
            filtro1.Campo = "INST_ID";
            filtro1.TipoDato = TipoDatoGeneral.Entero;
            filtro1.Valor = instId.ToString();

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);
            filtros.Add(filtro1);
            VCFramework.Entidad.TokenUsuario retorno = new Entidad.TokenUsuario();

            List<object> lista = fac.Leer<VCFramework.Entidad.TokenUsuario>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.TokenUsuario> lista2 = new List<VCFramework.Entidad.TokenUsuario>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.TokenUsuario>().ToList();
            }
            if (lista2 != null && lista2.Count > 0)
            {
                int id = lista2.Max(p => p.Id);
                retorno = lista2.Find(p=>p.Id == id);
            }

            return retorno;
        }
    }
}
