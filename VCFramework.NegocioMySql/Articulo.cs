using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class Articulo
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static int Modificar(Entidad.Articulo item)
        {
            int retorno = 0;

            item.Modificado = true;
            item.Nuevo = false;
            item.Borrado = false;

            Factory fac = new Factory();

            retorno = fac.Update<Entidad.Articulo>(item, setCnsWebLun);

            return retorno;
        }

        public static List<VCFramework.Entidad.Articulo> Listar(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Articulo>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Articulo> lista2 = new List<VCFramework.Entidad.Articulo>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Articulo>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }

    }
}
