using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class ArchivoAdjunto
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.ArchivoAdjunto> Listar()
        {
            List<VCFramework.Entidad.ArchivoAdjunto> retorno = new List<Entidad.ArchivoAdjunto>();

            VCFramework.Entidad.ArchivoAdjunto arch1 = new Entidad.ArchivoAdjunto();
            arch1.Id = 1;
            arch1.NombreArchivo = "BB__4.png";
            arch1.TipoPadre = 1; //novedades
            retorno.Add(arch1);

            VCFramework.Entidad.ArchivoAdjunto arch2 = new Entidad.ArchivoAdjunto();
            arch2.Id = 2;
            arch2.NombreArchivo = "cancha_1.jpg";
            arch2.TipoPadre = 1; //novedades
            retorno.Add(arch2);

            VCFramework.Entidad.ArchivoAdjunto arch3 = new Entidad.ArchivoAdjunto();
            arch1.Id = 3;
            arch1.NombreArchivo = "cancha_2.jpg";
            arch1.TipoPadre = 1; //novedades
            retorno.Add(arch3);


            return retorno;
        }
        public static List<VCFramework.Entidad.ArchivoAdjunto> Listar(int instId, int elementoId, int tipoPadre)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            FiltroGenerico filtro1 = new FiltroGenerico();
            filtro1.Campo = "ELEMENTO_ID";
            filtro1.Valor = elementoId.ToString();
            filtro1.TipoDato = TipoDatoGeneral.Entero;

            FiltroGenerico filtro2 = new FiltroGenerico();
            filtro2.Campo = "TIPO_PADRE";
            filtro2.Valor = tipoPadre.ToString();
            filtro2.TipoDato = TipoDatoGeneral.Entero;

            filtros.Add(filtro);
            filtros.Add(filtro1);
            filtros.Add(filtro2);


            List<object> lista = fac.Leer<VCFramework.Entidad.ArchivoAdjunto>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.ArchivoAdjunto> lista2 = new List<VCFramework.Entidad.ArchivoAdjunto>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.ArchivoAdjunto>().ToList();
            }
          
            return lista2;
        }
        public static VCFramework.Entidad.ArchivoAdjunto BuscarPorId(int id)
        {
            VCFramework.Entidad.ArchivoAdjunto entidad = new Entidad.ArchivoAdjunto();
            List<VCFramework.Entidad.ArchivoAdjunto> archivos = new List<Entidad.ArchivoAdjunto>();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            filtros.Add(filtro);

            List<object> lista = fac.Leer<VCFramework.Entidad.ArchivoAdjunto>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.ArchivoAdjunto> lista2 = new List<VCFramework.Entidad.ArchivoAdjunto>();
            if (lista != null)
            {

                archivos = lista.Cast<VCFramework.Entidad.ArchivoAdjunto>().ToList();
            }
            if (archivos != null && archivos.Count == 1)
            {
                entidad = archivos[0];
            }

            return entidad;
        }
        public static int Insertar(VCFramework.Entidad.ArchivoAdjunto entidad)
        {
            Factory fac = new Factory();
            return fac.Insertar<VCFramework.Entidad.ArchivoAdjunto>(entidad, setCnsWebLun);
        }

        public static int Eliminar(VCFramework.Entidad.ArchivoAdjunto entidad)
        {
            Factory fac = new Factory();
            return fac.Delete<VCFramework.Entidad.ArchivoAdjunto>(entidad, setCnsWebLun);
        }
        public static int Actualizar(VCFramework.Entidad.ArchivoAdjunto entidad)
        {
            Factory fac = new Factory();
            return fac.Update<VCFramework.Entidad.ArchivoAdjunto>(entidad, setCnsWebLun);
        }
    }
}
