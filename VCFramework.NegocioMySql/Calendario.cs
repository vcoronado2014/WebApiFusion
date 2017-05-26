using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net.Mail;
using VCFramework.Negocio.Factory;
using System.Data.SqlClient;
using System.Data;

namespace VCFramework.NegocioMySQL
{
    public class Calendario
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.Calendario> ObtenerCalendarioPorInstId(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Calendario>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Calendario> lista2 = new List<VCFramework.Entidad.Calendario>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Calendario>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static List<VCFramework.Entidad.Calendario> ObtenerCalendarioPorId(int id)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Calendario>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Calendario> lista2 = new List<VCFramework.Entidad.Calendario>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Calendario>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static List<VCFramework.Entidad.Calendario> ObtenerCalendarioPorInstidTipo(int instId, int tipo)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            FiltroGenerico filtro2 = new FiltroGenerico();
            filtro2.Campo = "TIPO";
            filtro2.Valor = tipo.ToString();
            filtro2.TipoDato = TipoDatoGeneral.Entero;

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);
            filtros.Add(filtro2);

            List<object> lista = fac.Leer<VCFramework.Entidad.Calendario>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.Calendario> lista2 = new List<VCFramework.Entidad.Calendario>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Calendario>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static void EliminarEvento(int AppointmentId, int Status, int Id, int Tipo)
        {
            if (Tipo == 0)
            {
                Factory fac = new Factory();
                #region modificar calendario
                //primero lo buscamos
                List<Entidad.Calendario> listaCal = NegocioMySQL.Calendario.ObtenerCalendarioPorId(Id);
                Entidad.Calendario cal = new Entidad.Calendario();
                if (listaCal != null && listaCal.Count > 0)
                {
                    cal = listaCal[0];

                }
                if (cal != null && cal.Id > 0)
                {

                    Entidad.Calendario calendario = new Entidad.Calendario();
                    cal.Eliminado = 1;
                    cal.Nuevo = false;
                    cal.Modificado = true;
                    cal.Borrado = false;
                    fac.Update<Entidad.Calendario>(cal, setCnsWebLun);
                }
                #endregion
            }
            else
                throw new Exception("No puede Eliminar un Evento del Tipo Proyecto.");
        }
        public static void CrearEvento(int Id, string Descripcion, DateTime FechaInicio, DateTime FechaTermino,
            int Status, string Asunto, string Ubicacion, int Etiqueta, int Tipo, int InstId, bool Borrado)
        {
            if (Tipo == 0)
            {
                Factory fac = new Factory();
                Entidad.Calendario calendario = new Entidad.Calendario();
                calendario.Asunto = Asunto;
                try {
                    if (Descripcion == null)
                        Descripcion = "Sin Descripción";

                    calendario.Descripcion = Descripcion;
                    calendario.Etiqueta = Etiqueta;
                    calendario.FechaInicio = FechaInicio;
                    calendario.FechaTermino = FechaTermino;
                    //ver como sacar la institucion
                    calendario.InstId = InstId;
                    calendario.Status = Status;
                    calendario.Tipo = Tipo;
                    if (Ubicacion == null)
                        Ubicacion = "";
                    calendario.Ubicacion = Ubicacion;
                    calendario.Url = "";
                    

                    calendario.Detalle = Descripcion;
                    calendario.Titulo = Descripcion;

                    fac.Insertar<Entidad.Calendario>(calendario, setCnsWebLun);

                    if (NegocioMySQL.Utiles.ENVIA_CORREO_EVENTO(InstId) == "1")
                    {

                        List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(InstId);

                        List<string> listaCorreos = new List<string>();
                        if (correos != null && correos.Count > 0)
                        {
                            foreach (UsuariosCorreos us in correos)
                            {
                                listaCorreos.Add(us.Correo);
                            }
                        }
                        if (listaCorreos != null && listaCorreos.Count > 0)
                        {
                            NegocioMySQL.ServidorCorreo cr = new NegocioMySQL.ServidorCorreo();
                            Entidad.Institucion institucion = NegocioMySQL.Institucion.ObtenerInstitucionPorId(InstId);
                            string fecha1 = Utiles.ConstruyeFecha(FechaInicio);
                            string fecha2 = Utiles.ConstruyeFecha(FechaTermino);
                            MailMessage mnsj = NegocioMySQL.Utiles.ConstruyeMensajeCrearEvento(institucion.Nombre, Asunto, fecha1 + " " + fecha2, Ubicacion, listaCorreos, true);
                            cr.Enviar(mnsj);
                        }

                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
                throw new Exception("No puede crear un Evento del Tipo Proyecto.");

        }
        public static void UpdateEvento(int Id, string Descripcion, DateTime FechaInicio, DateTime FechaTermino,
            int Status, string Asunto, string Ubicacion, int Etiqueta, int Tipo, int InstId, bool Borrado)
        {
            if (Tipo == 0)
            {
                Factory fac = new Factory();
                Entidad.Calendario calendario = new Entidad.Calendario();
                if (Descripcion == null)
                    Descripcion = "Sin descripción";
                calendario.Id = Id;
                calendario.Asunto = Asunto;
                calendario.Descripcion = Descripcion;
                calendario.Etiqueta = Etiqueta;
                calendario.FechaInicio = FechaInicio;
                calendario.FechaTermino = FechaTermino;
                //ver como sacar la institucion
                calendario.InstId = InstId;
                calendario.Status = Status;
                calendario.Tipo = Tipo;
                if (Ubicacion == null)
                    Ubicacion = "";
                calendario.Ubicacion = Ubicacion;
                calendario.Url = "";

                calendario.Nuevo = false;
                calendario.Modificado = true;
                fac.Update<Entidad.Calendario>(calendario, setCnsWebLun);
                if (NegocioMySQL.Utiles.ENVIA_CORREO_EVENTO(InstId) == "1")
                {

                    List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(InstId);

                    List<string> listaCorreos = new List<string>();
                    if (correos != null && correos.Count > 0)
                    {
                        foreach (UsuariosCorreos us in correos)
                        {
                            listaCorreos.Add(us.Correo);
                        }
                    }
                    if (listaCorreos != null && listaCorreos.Count > 0)
                    {
                        NegocioMySQL.ServidorCorreo cr = new NegocioMySQL.ServidorCorreo();
                        Entidad.Institucion institucion = NegocioMySQL.Institucion.ObtenerInstitucionPorId(InstId);
                        string fecha1 = Utiles.ConstruyeFecha(FechaInicio);
                        string fecha2 = Utiles.ConstruyeFecha(FechaTermino);
                        MailMessage mnsj = NegocioMySQL.Utiles.ConstruyeMensajeCrearEvento(institucion.Nombre, Asunto, fecha1 + " " + fecha2, Ubicacion, listaCorreos, false);
                        cr.Enviar(mnsj);
                    }

                }
            }
            else
                throw new Exception("No puede Modificar un Evento del Tipo Proyecto.");

        }
        public static string ObtenerCalendarioInstJson(int instId)
        {

            string json = "";
            List<VCFramework.Entidad.Calendario> lista = ObtenerCalendarioPorInstId(instId);
            List<events> events = new List<events>();
            if (lista != null && lista.Count > 0)
            {



                foreach (VCFramework.Entidad.Calendario cal in lista)
                {
                    events.Add(new events
                    {
                        
                        title = cal.Titulo,
                        start = Utiles.ConstruyeFecha(cal.FechaInicio),
                        end = Utiles.ConstruyeFecha(cal.FechaTermino),
                        url = cal.Url
                    });
                }





            }

            json = JsonConvert.SerializeObject(events);
            return json;
        }
        public static int Insertar(Entidad.Calendario entidad)
        {
            int retorno = 0;
            Factory fac = new Factory();
            retorno = fac.Insertar<Entidad.Calendario>(entidad, setCnsWebLun);

            return retorno;
        }
        public static int Modificar(Entidad.Calendario entidad)
        {
            int retorno = 0;
            Factory fac = new Factory();
            retorno = fac.Update<Entidad.Calendario>(entidad, setCnsWebLun);

            return retorno;
        }

        public static EntidadValida ValidaEvento(int fechaEnteraInicio, int fechaEnteraTermino, int instId, int usuIdCreador, string nombre)
        {
            EntidadValida retorno = new EntidadValida();

            string valor = "";
            int tipo = 0;
            string conexionStr = setCnsWebLun.ConnectionString;
            SqlConnection conn = new SqlConnection(conexionStr);
            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(conexionStr);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "VALIDA_EVENTO";

            cmd.Parameters.AddWithValue("@FECHA_ENTERA_INICIO", fechaEnteraInicio);
            cmd.Parameters.AddWithValue("@FECHA_ENTERA_TERMINO", fechaEnteraTermino);
            cmd.Parameters.AddWithValue("@INST_ID", instId);
            cmd.Parameters.AddWithValue("@USU_ID_CREADOR", usuIdCreador);
            cmd.Parameters.AddWithValue("@NOMBRE", nombre);
            cmd.Connection.Open();

            try
            {
                SqlDataReader rdr = cmd.ExecuteReader();
                int NOMBRE = rdr.GetOrdinal("NOMBRE");
                int TIPO = rdr.GetOrdinal("TIPO");
                try
                {
                    while (rdr.Read())
                    {
                        valor = rdr.IsDBNull(NOMBRE) ? "" : rdr.GetString(NOMBRE);
                        tipo = rdr.IsDBNull(TIPO) ? 0 : rdr.GetInt32(TIPO);
                        retorno.Valor = valor;
                        retorno.Tipo = tipo;
                        break;
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

            return retorno;
        }


    }
    public class EntidadValida
    {
        public string Valor { get; set; }
        public int Tipo { get; set; }
    }

    public class events
    {
        public events()
        {

        }

        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string url { get; set; }
    }
    public class UsuariosCorreos
    {
        public string Correo { get; set; }
        public string Nombre { get; set; }

        public static List<UsuariosCorreos> ListaUsuariosCorreosPorInstId(int instId)
        {
            List<UsuariosCorreos> retorno = new List<UsuariosCorreos>();

            try
            {

                List<Entidad.AutentificacionUsuario> listaUsuarios = NegocioMySQL.AutentificacionUsuario.ListarUsuariosPorInstId(instId);
                if (listaUsuarios != null && listaUsuarios.Count > 0)
                {
                    foreach (Entidad.AutentificacionUsuario au in listaUsuarios)
                    {
                        if (au.CorreoElectronico != "")
                        {
                            if (!retorno.Exists(p => p.Correo == au.CorreoElectronico))
                            {
                                UsuariosCorreos us = new UsuariosCorreos();
                                us.Correo = au.CorreoElectronico;
                                //us.Nombre = au.Persona.Nombres + " " + au.Persona.ApellidoPaterno + " " + au.Persona.ApellidoMaterno;
                                Entidad.Persona persona =  NegocioMySQL.Persona.ObtenerPersonaPorUsuId(au.Id);
                                if (persona != null && persona.Id > 0)
                                    us.Nombre = persona.Nombres + " " + persona.ApellidoPaterno + " " + persona.ApellidoMaterno;
                                else
                                    us.Nombre = au.NombreUsuario;
                                retorno.Add(us);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utiles.Log(ex);
            }

            return retorno;
        }
    }
}
