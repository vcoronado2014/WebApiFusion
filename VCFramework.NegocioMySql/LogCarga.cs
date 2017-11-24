using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class LogCarga
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];

        public static int Insertar(VCFramework.Entidad.LogCarga entidad)
        {
            Factory fac = new Factory();
            return fac.Insertar<VCFramework.Entidad.LogCarga>(entidad, setCnsWebLun);
        }
        public static List<VCFramework.Entidad.LogCarga> ListarPorEncabezadoId(int eccId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ECC_ID";
            filtro.TipoDato = TipoDatoGeneral.Entero;
            filtro.Valor = eccId.ToString();

            List<object> lista = fac.Leer<VCFramework.Entidad.LogCarga>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.LogCarga> lista2 = new List<VCFramework.Entidad.LogCarga>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.LogCarga>().ToList();
            }

            return lista2;
        }

    }
}
