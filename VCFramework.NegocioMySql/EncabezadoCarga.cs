using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCFramework.Negocio.Factory;


namespace VCFramework.NegocioMySql
{
    public class EncabezadoCarga
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static int Insertar(VCFramework.Entidad.EncabezadoCarga entidad)
        {
            Factory fac = new Factory();
            return fac.Insertar<VCFramework.Entidad.EncabezadoCarga>(entidad, setCnsWebLun);
        }
        public static List<VCFramework.Entidad.EncabezadoCarga> ListarPorInstitucion(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.TipoDato = TipoDatoGeneral.Entero;
            filtro.Valor = instId.ToString();

            List<object> lista = fac.Leer<VCFramework.Entidad.EncabezadoCarga>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.EncabezadoCarga> lista2 = new List<VCFramework.Entidad.EncabezadoCarga>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.EncabezadoCarga>().ToList();
            }

            return lista2;
        }

    }
}
