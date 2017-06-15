using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySql
{
    public class Reportes
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

        public static List<VCFramework.EntidadFuncional.ReportesFuncional> ObtenerReporteVotaciones(int instId, int rolId)
        {
            string conexionStr = setCnsWebLun.ConnectionString;
            SqlConnection conn = new SqlConnection(conexionStr);
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(conexionStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "OBTENER_REPORTE_VOTACIONES";

            cmd.Connection.Open();

            List<VCFramework.EntidadFuncional.ReportesFuncional> lista = new List<EntidadFuncional.ReportesFuncional>();

            try
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                int INST_ID = rdr.GetOrdinal("INST_ID");
                int INSTITUCION = rdr.GetOrdinal("INSTITUCION");
                int FECHAS = rdr.GetOrdinal("FECHAS");
                int NOMBRE_TRICEL = rdr.GetOrdinal("NOMBRE_TRICEL");
                int NOMBRE_LISTA = rdr.GetOrdinal("NOMBRE_LISTA");
                int CANTIDAD_VOTOS = rdr.GetOrdinal("CANTIDAD_VOTOS");
                int CANTIDAD_VOTOS_NO = rdr.GetOrdinal("CANTIDAD_VOTOS_NO");
                int TIPO = rdr.GetOrdinal("TIPO");
                try
                {
                    while (rdr.Read())
                    {
                        VCFramework.EntidadFuncional.ReportesFuncional entidad = new EntidadFuncional.ReportesFuncional();
                        entidad.InstId = rdr.IsDBNull(INST_ID) ? 0 : rdr.GetInt32(INST_ID);
                        entidad.Institucion = rdr.IsDBNull(INSTITUCION) ? "" : rdr.GetString(INSTITUCION);
                        entidad.Fechas = rdr.IsDBNull(FECHAS) ? "" : rdr.GetString(FECHAS);
                        entidad.Nombre = rdr.IsDBNull(NOMBRE_TRICEL) ? "" : rdr.GetString(NOMBRE_TRICEL);
                        entidad.Lista = rdr.IsDBNull(NOMBRE_LISTA) ? "" : rdr.GetString(NOMBRE_LISTA);
                        entidad.VotosSi = rdr.IsDBNull(CANTIDAD_VOTOS) ? 0 : rdr.GetInt32(CANTIDAD_VOTOS);
                        entidad.VotosNo = rdr.IsDBNull(CANTIDAD_VOTOS_NO) ? 0 : rdr.GetInt32(CANTIDAD_VOTOS_NO);
                        entidad.Tipo = rdr.IsDBNull(TIPO) ? "" : rdr.GetString(TIPO);

                        lista.Add(entidad);
                    }
                }
                finally
                {
                    rdr.Close();
                }
            }
            finally
            {
                cmd.Connection.Close();
            }

            if (lista != null && lista.Count > 0)
            {
                if (rolId != 1)
                {
                    lista = lista.FindAll(p => p.InstId == instId);
                }
            }

            return lista;
        }

    }
}
