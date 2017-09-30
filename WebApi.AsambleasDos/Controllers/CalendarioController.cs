using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VCFramework.Entidad;
using VCFramework.NegocioMySQL;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Xml;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using System.Net.Mail;

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CalendarioController : ApiController
    {

        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]string instId, [FromUri]string usuIdCreador, [FromUri]string fechaInicioEntera, [FromUri]string fechaTerminoEntera, [FromUri]string nombre)
        {

            //validaciones antes de ejecutar la llamada.
            if (usuIdCreador == "")
                throw new ArgumentNullException("Id");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                int instIdInt = int.Parse(instId);
                int usuIdCreadorInt = int.Parse(usuIdCreador);
                int fechaInicioEnteraInt = int.Parse(fechaInicioEntera);
                int fechaTerminoEnteraInt = int.Parse(fechaTerminoEntera);
                string nombreStr = nombre;

                //ahora traemos los eventos

                VCFramework.NegocioMySQL.EntidadValida consulta = VCFramework.NegocioMySQL.Calendario.ValidaEvento(fechaInicioEnteraInt, fechaTerminoEnteraInt, instIdInt, usuIdCreadorInt, nombreStr);


                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(consulta);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);
            }
            catch (Exception ex)
            {
                httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                throw ex;
            }
            return httpResponse;
        }

        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {
            Random rnd = new Random();

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);


            //validaciones antes de ejecutar la llamada.
            if (data.InstId == 0)
                throw new ArgumentNullException("InstId");

            List<VCFramework.Entidad.Calendario> eventos = new List<VCFramework.Entidad.Calendario>();

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string instId = data.InstId;
                int instIdBuscar = int.Parse(instId);
                string tipo = data.Tipo;
                if (tipo == "0")
                    eventos = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstId(instIdBuscar);
                else
                    eventos = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstidTipo(instIdBuscar, int.Parse(tipo));
                //HAY QUE TRAER TAMBIEN LOS PROYECTOS QUE FUERON CREADOS
                //List<VCFramework.Entidad.Proyectos> proyectos = VCFramework.NegocioMySQL.Proyectos.ObtenerProyectosPorInstIdN(instIdBuscar);

                List<WebApi.AsambleasDos.Controllers.evento> lista = new List<WebApi.AsambleasDos.Controllers.evento>();
                if (eventos != null && eventos.Count > 0)
                {


                    foreach (VCFramework.Entidad.Calendario cal in eventos)
                    {
                        WebApi.AsambleasDos.Controllers.evento entidad = new WebApi.AsambleasDos.Controllers.evento();
                        entidad.allDay = false;
                        entidad.content = cal.Titulo;

                        entidad.annoIni = cal.FechaInicio.Year.ToString();
                        entidad.mesIni = (cal.FechaInicio.Month - 1).ToString();
                        entidad.diaIni = cal.FechaInicio.Day.ToString();
                        entidad.horaIni = cal.FechaInicio.Hour.ToString();
                        entidad.minutosIni = cal.FechaInicio.Minute.ToString();

                        entidad.annoTer = cal.FechaTermino.Year.ToString();
                        entidad.mesTer = (cal.FechaTermino.Month - 1).ToString();
                        entidad.diaTer = cal.FechaTermino.Day.ToString();
                        entidad.horaTer = cal.FechaTermino.Hour.ToString();
                        entidad.minutosTer = cal.FechaTermino.Minute.ToString();
                        //important = 3, success = 2, info = 1 
                        if (cal.Etiqueta == 1)
                            entidad.className = "info";
                        else if (cal.Etiqueta == 2)
                            entidad.className = "success";
                        else if (cal.Etiqueta == 3)
                            entidad.className = "important";
                        else
                            entidad.className = "";

                        entidad.id = rnd.Next(1, 300);
                        entidad.clientId = cal.Id;
                        lista.Add(entidad);
                    }

                }
                //se cambio la implementacion ahora se obtiene todo del calendario
                #region comentado

                //if (proyectos != null && proyectos.Count > 0)
                //{
                //    if (tipo == "1")
                //    {
                //        foreach (VCFramework.Entidad.Proyectos proy in proyectos)
                //        {
                //            WebApi.Asambleas.Controllers.evento entidad = new WebApi.Asambleas.Controllers.evento();
                //            entidad.allDay = false;
                //            entidad.content = proy.Nombre;

                //            string[] fechasIni = proy.FechaInicio.Split('-');

                //            entidad.annoIni = int.Parse(fechasIni[2]).ToString();
                //            entidad.mesIni = int.Parse(fechasIni[1]).ToString();
                //            entidad.diaIni = int.Parse(fechasIni[0]).ToString();
                //            entidad.horaIni = "23";
                //            entidad.minutosIni = "59";

                //            string[] fechasTer = proy.FechaTermino.Split('-');
                //            entidad.annoTer = int.Parse(fechasTer[2]).ToString();
                //            entidad.mesTer = int.Parse(fechasTer[1]).ToString();
                //            entidad.diaTer = int.Parse(fechasTer[0]).ToString();
                //            entidad.horaTer = "23";
                //            entidad.minutosTer = "59";

                //            entidad.id = rnd.Next(301, 3000);
                //            entidad.clientId = entidad.id;
                //            lista.Add(entidad);
                //        }
                //    }
                //    else
                //    {
                //        //no debe pasar por acá en el tipo 0
                //        #region comentado
                //        //foreach (VCFramework.Entidad.Proyectos proy in proyectos)
                //        //{
                //        //    //por cada fecha un evento
                //        //    //acá manda error en la conversión
                //        //    //con un formato por ejemplo 23-06-2017, 
                //        //    //DateTime fechaInicio = new DateTime()
                //        //    string[] fechaArrInicio = proy.FechaInicio.Split('-');
                //        //    int diaInicioInt = VCFramework.NegocioMySQL.Utiles.EntregaEntero(fechaArrInicio[0]);
                //        //    int mesInicioInt = VCFramework.NegocioMySQL.Utiles.EntregaEntero(fechaArrInicio[1]);
                //        //    int annoInicioInt = int.Parse(fechaArrInicio[2]);
                //        //    DateTime fechaInicio = new DateTime(annoInicioInt, mesInicioInt, diaInicioInt);
                //        //    //DateTime fechaInicio = Convert.ToDateTime(proy.FechaInicio);

                //        //    string[] fechaArrTermino = proy.FechaTermino.Split('-');
                //        //    int diaTerminoInt = VCFramework.NegocioMySQL.Utiles.EntregaEntero(fechaArrTermino[0]);
                //        //    int mesTerminoInt = VCFramework.NegocioMySQL.Utiles.EntregaEntero(fechaArrTermino[1]);
                //        //    int annoTerminoInt = int.Parse(fechaArrTermino[2]);
                //        //    DateTime fechaTermino = new DateTime(annoTerminoInt, mesTerminoInt, diaTerminoInt);

                //        //    //DateTime fechaTermino = Convert.ToDateTime(proy.FechaTermino);
                //        //    TimeSpan ts = fechaTermino - fechaInicio;
                //        //    DateTime fechitaInicio = DateTime.MinValue;
                //        //    int differenceInDays = ts.Days;
                //        //    for (int i=0; i<differenceInDays; i++)
                //        //    {
                //        //        if (i==0)
                //        //            fechitaInicio = Convert.ToDateTime(fechaInicio.ToShortDateString() + " 08:00");
                //        //        else
                //        //            fechitaInicio = Convert.ToDateTime(fechitaInicio.ToShortDateString() + " 08:00");

                //        //        DateTime fechitaTermino = fechitaInicio.AddHours(10);
                //        //        WebApi.Asambleas.Controllers.evento entidad = new WebApi.Asambleas.Controllers.evento();
                //        //        entidad.allDay = true;
                //        //        entidad.content = proy.Nombre;

                //        //        entidad.annoIni = fechitaInicio.Year.ToString();
                //        //        entidad.mesIni = (fechitaInicio.Month - 1).ToString();
                //        //        entidad.diaIni = fechitaInicio.Day.ToString();
                //        //        entidad.horaIni = fechitaInicio.Hour.ToString();
                //        //        entidad.minutosIni = fechitaInicio.Minute.ToString();

                //        //        entidad.annoTer = fechitaTermino.Year.ToString();
                //        //        entidad.mesTer = (fechitaTermino.Month - 1).ToString();
                //        //        entidad.diaTer = fechitaTermino.Day.ToString();
                //        //        entidad.horaTer = fechitaTermino.Hour.ToString();
                //        //        entidad.minutosTer = fechitaTermino.Minute.ToString();

                //        //        entidad.id = rnd.Next(301, 3000);
                //        //        entidad.clientId = entidad.id;
                //        //        fechitaInicio = fechitaInicio.AddDays(1);
                //        //        lista.Add(entidad);
                //        //    }
                //        //}
                //        #endregion
                //    }
                //}
                #endregion

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(lista);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);

            }
            catch (Exception ex)
            {
                httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                throw ex;
            }
            return httpResponse;


        }

        [System.Web.Http.AcceptVerbs("PUT")]
        public HttpResponseMessage Put(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string idUsuario = data.IdUsuario;
            if (idUsuario == null)
                idUsuario = "0";

            string instId = data.InstId;
            string titulo = data.Titulo;
            string fechaInicio = data.FechaInicio;
            string fechaTermino = data.FechaTermino;
            string esNuevo = data.EsNuevo;
            string usuIdCreador = data.UsuIdCreador;
            string id = "0";
            string etiqueta = "0";
            if (data.Etiqueta != null)
                etiqueta = data.Etiqueta;
            if (data.Id != null)
                id = data.Id;


            HttpResponseMessage httpResponse = new HttpResponseMessage();

            //buscamos el evento con todos los parametros para ver si es nuevo o antiguo
            bool nuevoRegistro = Convert.ToBoolean(esNuevo);
            int idNuevo = 0;



            try
            {
                VCFramework.Entidad.Calendario calendario = new VCFramework.Entidad.Calendario();
				System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

                //si es nuevo registro se inserta y no se realiza busqueda
                if (nuevoRegistro)
                {
                    calendario.Asunto = titulo;
                    calendario.Descripcion = titulo;
                    calendario.Detalle = titulo;
                    calendario.Eliminado = 0;
                    calendario.Etiqueta = int.Parse(etiqueta);
					calendario.FechaInicio = DateTime.Parse(fechaInicio, culture);
					calendario.FechaTermino = DateTime.Parse(fechaTermino, culture);
                    //calendario.FechaTermino = Convert.ToDateTime(fechaTermino);
                    calendario.InstId = int.Parse(instId);
                    calendario.Status = 1;
                    calendario.Tipo = 1;
                    calendario.Titulo = titulo;
                    calendario.Ubicacion = "";
                    calendario.Url = "";
                    calendario.UsuIdCreador = int.Parse(usuIdCreador);
                    //insertar
                    idNuevo = VCFramework.NegocioMySQL.Calendario.Insertar(calendario);
                    calendario.Id = idNuevo;

                    List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(int.Parse(instId));
                    List<string> listaCorreos = new List<string>();
                    if (correos != null && correos.Count > 0)
                    {
                        foreach (UsuariosCorreos us in correos)
                        {
                            if (!listaCorreos.Exists(p => p == us.Correo))
                                listaCorreos.Add(us.Correo);
                        }
                    }
                    if (listaCorreos != null && listaCorreos.Count > 0)
                    {
                        VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(int.Parse(instId));
                        VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                        //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, false);
                        MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeEvento(institucion.Id, institucion.Nombre, titulo, "No definida", calendario.FechaInicio.ToShortDateString() + " " + calendario.FechaInicio.ToShortTimeString() + " - " + calendario.FechaTermino.ToShortDateString() + " " + calendario.FechaTermino.ToShortTimeString(), listaCorreos, true, false, false);

                        //cr.Enviar(mnsj);
                        var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                    }

                }
                else
                {
                    List<VCFramework.Entidad.Calendario> calendarios = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorId(int.Parse(id));
                    calendario = calendarios[0];
                    if (calendario != null && calendario.Id > 0)
                    {
                        calendario.UsuIdCreador = int.Parse(usuIdCreador);
                        calendario.Asunto = titulo;
                        calendario.Descripcion = titulo;
                        calendario.Detalle = titulo;
                        calendario.Titulo = titulo;
                        calendario.FechaInicio = DateTime.Parse(fechaInicio, culture);
                        calendario.FechaTermino = DateTime.Parse(fechaTermino, culture);
                        calendario.Etiqueta = int.Parse(etiqueta);
                        VCFramework.NegocioMySQL.Calendario.Modificar(calendario);

                        List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(int.Parse(instId));
                        List<string> listaCorreos = new List<string>();
                        if (correos != null && correos.Count > 0)
                        {
                            foreach (UsuariosCorreos us in correos)
                            {
                                if (!listaCorreos.Exists(p => p == us.Correo))
                                    listaCorreos.Add(us.Correo);
                            }
                        }
                        if (listaCorreos != null && listaCorreos.Count > 0)
                        {
                            VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(int.Parse(instId));
                            VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                            //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, false);
                            MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeEvento(institucion.Id, institucion.Nombre, titulo, "No definida", calendario.FechaInicio.ToShortDateString() + " " + calendario.FechaInicio.ToShortTimeString() + " - " + calendario.FechaTermino.ToShortDateString() + " " + calendario.FechaTermino.ToShortTimeString(), listaCorreos, false, true, false);

                            //cr.Enviar(mnsj);
                            var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                        }

                    }
                }


                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(calendario);
                httpResponse.Content = new StringContent(JSON);
                httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);

            }
            catch (Exception ex)
            {
                httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                throw ex;
            }

            return httpResponse;
        }

        [System.Web.Http.AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string idUsuario = data.IdUsuario;
            if (idUsuario == null)
                idUsuario = "0";

            string instId = data.InstId;
            string titulo = data.Titulo;
            string fechaInicio = data.FechaInicio;
            string fechaTermino = data.FechaTermino;
            string id = "0";
            if (data.Id != null)
                id = data.Id;


            HttpResponseMessage httpResponse = new HttpResponseMessage();


            try
            {
                VCFramework.Entidad.Calendario calendario = new VCFramework.Entidad.Calendario();
                if (int.Parse(id) > 0)
                {
                    calendario = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorId(int.Parse(id))[0];
                }
                else
                {
                    List<VCFramework.Entidad.Calendario> calendarios = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstId(int.Parse(instId));
                    calendario = calendarios.Find(p => p.Titulo.Replace("\t", "").Replace(" ", "").ToUpper() == titulo.Replace("\t", "").Replace(" ", "").ToUpper() && VCFramework.NegocioMySQL.Utiles.RetornaFechaEntera(p.FechaInicio) == VCFramework.NegocioMySQL.Utiles.RetornaFechaEnteraStr(fechaInicio)
                        && VCFramework.NegocioMySQL.Utiles.RetornaFechaEntera(p.FechaTermino) == VCFramework.NegocioMySQL.Utiles.RetornaFechaEnteraStr(fechaTermino));

                }

                if (calendario != null && calendario.Id > 0)
                {
                    calendario.Eliminado = 1;
                    VCFramework.NegocioMySQL.Calendario.Modificar(calendario);

                    List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(int.Parse(instId));
                    List<string> listaCorreos = new List<string>();
                    if (correos != null && correos.Count > 0)
                    {
                        foreach (UsuariosCorreos us in correos)
                        {
                            if (!listaCorreos.Exists(p => p == us.Correo))
                                listaCorreos.Add(us.Correo);
                        }
                    }
                    if (listaCorreos != null && listaCorreos.Count > 0)
                    {
                        VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(int.Parse(instId));
                        VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                        //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, false);
                        MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeEvento(institucion.Id, institucion.Nombre, titulo, "No definida", calendario.FechaInicio.ToShortDateString() + " " + calendario.FechaInicio.ToShortTimeString() + " - " + calendario.FechaTermino.ToShortDateString() + " " + calendario.FechaTermino.ToShortTimeString(), listaCorreos, false, false, true);

                        //cr.Enviar(mnsj);
                        var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                    }

                }


                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(calendario);
                httpResponse.Content = new StringContent(JSON);
                httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);

            }
            catch (Exception ex)
            {
                httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                throw ex;
            }

            return httpResponse;
        }
    }

    public class evento
    {

        public bool allDay { get; set; }

        public string content { get; set; }

        public string annoIni { get; set; }
        public string mesIni { get; set; }
        public string diaIni { get; set; }
        public string horaIni { get; set; }

        public string minutosIni { get; set; }

        public string annoTer { get; set; }
        public string mesTer { get; set; }
        public string diaTer { get; set; }
        public string horaTer { get; set; }
        public string minutosTer { get; set; }

        public int clientId { get; set; }
        public int id { get; set; }
        public string className { get; set; }

    }

}