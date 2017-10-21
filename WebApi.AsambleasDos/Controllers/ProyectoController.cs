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
using System.IO;
using System.Text;
using System.Web;
using System.Net.Mail;
using System.Globalization;

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProyectoController : ApiController
    {
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);
            string usuId = "0";

            string buscarNombreUsuario = "";
            if (data.BuscarId != null)
            {
                buscarNombreUsuario = data.BuscarId;
            }

            //validaciones antes de ejecutar la llamada.
            if (data.InstId == 0)
                throw new ArgumentNullException("InstId");
            if (data.UsuId != null)
                usuId = data.UsuId;

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string instId = data.InstId;
                int instIdBuscar = int.Parse(instId);
                string totalUsuarios = VCFramework.NegocioMySQL.AutentificacionUsuario.ListarUsuariosPorInstId(int.Parse(instId)).Count.ToString();

                VCFramework.EntidadFuncional.proposalss votaciones = new VCFramework.EntidadFuncional.proposalss();
                votaciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.Proyectos> triceles = VCFramework.NegocioMySQL.Proyectos.ObtenerProyectosPorInstIdTodos(instIdBuscar);

                if (buscarNombreUsuario != "")
                {
                    if (VCFramework.NegocioMySQL.Utiles.IsNumeric(buscarNombreUsuario))
                        triceles = triceles.FindAll(p => p.Id == int.Parse(buscarNombreUsuario));
                    else
                        triceles = triceles.FindAll(p => p.Nombre.ToUpper().Replace(" ", "") == buscarNombreUsuario.ToUpper().Replace(" ", ""));
                }


                if (triceles != null && triceles.Count > 0)
                {
                    foreach (VCFramework.Entidad.Proyectos tri in triceles)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = tri.Id;
                        us.NombreCompleto = tri.Objetivo;
                        us.NombreUsuario = tri.Nombre;
                        //3/28/2017
                        us.OtroUno = tri.FechaInicio;
                        us.OtroDos = tri.FechaTermino;


                        us.OtroTres = tri.FechaCreacion.ToShortDateString();
                        System.Globalization.CultureInfo cultura = System.Globalization.CultureInfo.GetCultureInfo("es-CL");
                        NumberFormatInfo nfi = new CultureInfo("es-CL", false).NumberFormat;
                        nfi.CurrencyDecimalDigits = 0;

                        //cultura.NumberFormat.CurrencyDecimalDigits = 0;
                        decimal result = tri.Costo / 1m;
                        string moneda = result.ToString("c", nfi);

                        us.OtroCuatro = tri.Costo.ToString();
                        us.OtroDiez = moneda;

                        //us.OtroCuatro = tri.Costo.ToString();

                        us.OtroCinco = us.OtroUno + " - " + us.OtroDos;
                        us.Rol = tri.Beneficios;
                        us.OtroSeis = tri.Descripcion;
                        int puedeVotar = VCFramework.NegocioMySQL.Proyectos.PuedeVotar(tri.Id);
                        us.OtroSiete = puedeVotar.ToString();

                        us.OtroOcho = us.OtroUno + " - " + us.OtroDos;

                        us.Url = "CrearModificarProyecto.html?id=" + us.Id.ToString() + "&ELIMINAR=0";
                        us.UrlEliminar = "CrearModificarProyecto.html?id=" + us.Id.ToString() + "&ELIMINAR=1";

                        //votaciones de las listas
                        int cantidadVotaciones = VCFramework.NegocioMySQL.Votaciones.ObtenerVotaciones(tri.Id).Count;
                        //aca la cantidad de votaciones
                        us.OtroOnce = cantidadVotaciones.ToString();
                        //aca el quorum
                        us.OtroDoce = tri.QuorumMinimo.ToString();
                        //total usuarios
                        us.TotalUsuarios = totalUsuarios;

                        //verificamos si ya votó
                        StringBuilder sb = new StringBuilder();
                        if (usuId != "0")
                        {
                            List<VCFramework.Entidad.Votaciones> listaVotaciones =  VCFramework.NegocioMySQL.Votaciones.ObtenerVotaciones(tri.Id, int.Parse(usuId));
                            if (listaVotaciones != null && listaVotaciones.Count == 1)
                            {
                                if (us.OtroSiete == "1")
                                    us.OtroSiete = "0";

                                VCFramework.Entidad.Votaciones voto = listaVotaciones[0];
                                sb.AppendFormat("Usted ya votó por este Proyecto el día {0}", voto.FechaVotacion.ToShortDateString() + " " + voto.FechaVotacion.ToShortTimeString());
                                if (voto.Valor == 1)
                                {
                                    sb.Append(", y se manifestó de acuerdo con el Proyecto.");
                                }
                                else
                                {
                                    sb.Append(", y manifestó NO estar de acuerdo con el Proyecto.");
                                }

                            }
                        }
                        else
                        {
                            sb.Append("Usted aún no ha votado este Proyecto.");
                        }
                        us.OtroNueve = sb.ToString();
                        us.QuorumMinimo = tri.QuorumMinimo.ToString();


                        votaciones.proposals.Add(us);
                    }
                    //establecimientos.Establecimientos = instituciones;
                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(votaciones);
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

            string id = data.Id;
            if (id == null)
                id = "0";

            string instId = data.InstId;
            string nombre = data.Nombre;
            string objetivo = data.Objetivo;
            string fechaInicio = data.FechaInicio;
            string fechaTermino = data.FechaTermino;
            string usuId = data.IdUsuario;
            string costo = data.Costo;
            string beneficios = data.Beneficios;
            string descripcion = data.Descripcion;
            string quorumMinimo = "0";

            if (data.QuorumMinimo != null)
            {
                quorumMinimo = data.QuorumMinimo;
            }

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            int idNuevo = 0;

            string esCpasStr = "false";
            bool esCpas = false;
            if (data.EsCpas != null)
            {
                esCpasStr = data.EsCpas;
                esCpas = Convert.ToBoolean(esCpasStr);
            }

            try
            {
                VCFramework.Entidad.Proyectos tricel = new VCFramework.Entidad.Proyectos();
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");
                IFormatProvider culture1 = new System.Globalization.CultureInfo("es-CL", true);
                if (id != "0")
                {
                    //es modificado
                    List<VCFramework.Entidad.Proyectos> triceles = VCFramework.NegocioMySQL.Proyectos.ObtenerProyectosPorId(int.Parse(id));
                    if (triceles.Count == 1)
                    {
                        tricel = triceles[0];
                        //tricel.FechaInicio = DateTime.Parse(fechaInicio, culture).ToShortDateString();
                        //tricel.FechaTermino = DateTime.Parse(fechaTermino, culture).ToShortDateString();
                        tricel.FechaInicio = fechaInicio;
                        tricel.FechaTermino = fechaTermino;
                        tricel.Nombre = nombre;
                        tricel.Objetivo = objetivo;
                        tricel.Costo = int.Parse(costo);
                        tricel.Beneficios = beneficios;
                        tricel.Descripcion = descripcion;
                        tricel.QuorumMinimo = int.Parse(quorumMinimo);
                        VCFramework.NegocioMySQL.Proyectos.Modificar(tricel);
                        if (tricel.Id >= 0)
                        {
                            #region manejo del evento
                            //ojo acá, hay que modificar el evento también
                            //para este caso vamos a utilizar el tipo 2 que será Proyecto
                            //el Status contendrá el id del elemento original
                            //falta agregar fechas
                            //24-03-2017
                            string[] fechaIniArr = tricel.FechaInicio.Split('-');
                            DateTime fechaIni = new DateTime(
                                int.Parse(fechaIniArr[2]),
                                int.Parse(fechaIniArr[1]),
                                int.Parse(fechaIniArr[0]),
                                0,0,0
                                );
                            string[] fechaTerArr = tricel.FechaTermino.Split('-');
                            DateTime fechaTer = new DateTime(
                                int.Parse(fechaTerArr[2]),
                                int.Parse(fechaTerArr[1]),
                                int.Parse(fechaTerArr[0]),
                                23, 59, 0
                                );

                            VCFramework.Entidad.Calendario calendario = new VCFramework.Entidad.Calendario();
                            List<VCFramework.Entidad.Calendario> listaCalendario = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstidTipo(tricel.InstId, 2);
                            if (listaCalendario != null && listaCalendario.Count > 0)
                            {

                                listaCalendario = listaCalendario.FindAll(p => p.Status == tricel.Id);
                                if (listaCalendario != null && listaCalendario.Count > 0)
                                {
                                    calendario = listaCalendario[0];
                                }
                            }
                            //seteamos los valores
                            calendario.Titulo = tricel.Nombre;
                            calendario.Detalle = tricel.Nombre;
                            calendario.Asunto = tricel.Nombre;
                            calendario.Url = "";
                            calendario.Ubicacion = "CPAS";
                            calendario.InstId = tricel.InstId;
                            calendario.Etiqueta = 1;
                            calendario.Descripcion = tricel.Nombre;
                            calendario.Status = tricel.Id;
                            calendario.Tipo = 2;
                            calendario.FechaInicio = fechaIni;
                            calendario.FechaTermino = fechaTer;
                            calendario.UsuIdCreador = tricel.UsuIdCreador;
                            if (calendario.Id > 0)
                            {
                                //MODIFICAR
                                VCFramework.NegocioMySQL.Calendario.Modificar(calendario);
                            }
                            else
                            {
                                VCFramework.NegocioMySQL.Calendario.Insertar(calendario);
                            }

                            #endregion


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
                                MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeProyecto(institucion.Id, institucion.Nombre, tricel.Nombre, listaCorreos, false, true, false, esCpas);

                                //cr.Enviar(mnsj);
                                var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                                }

                        }

                    }
                }
                else
                {
                    //es nuevo
                    tricel.Eliminado = 0;
                    tricel.EsVigente = 1;
                    //tricel.FechaInicio = DateTime.Parse(fechaInicio, culture).ToShortDateString();
                    //tricel.FechaTermino = DateTime.Parse(fechaTermino, culture).ToShortDateString();
                    tricel.FechaInicio = fechaInicio;
                    tricel.FechaTermino = fechaTermino;
                    tricel.Nombre = nombre;
                    tricel.Objetivo = objetivo;
                    tricel.InstId = int.Parse(instId);
                    tricel.UsuIdCreador = int.Parse(usuId);
                    tricel.Costo = int.Parse(costo);
                    tricel.EsVigente = 1;
                    tricel.FueAprobado = 1;
                    tricel.NotificaPopup = 1;
                    tricel.FechaCreacion = DateTime.Now;
                    tricel.Beneficios = beneficios;
                    tricel.Descripcion = descripcion;
                    tricel.QuorumMinimo = int.Parse(quorumMinimo);
                    int respuesta = tricel.Id = VCFramework.NegocioMySQL.Proyectos.Insertar(tricel);
                    if (respuesta >= 0)
                    {

                        #region manejo del evento
                        //ojo acá, hay que modificar el evento también
                        //para este caso vamos a utilizar el tipo 2 que será Proyecto
                        //el Status contendrá el id del elemento original
                        VCFramework.Entidad.Calendario calendario = new VCFramework.Entidad.Calendario();
                        List<VCFramework.Entidad.Calendario> listaCalendario = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstidTipo(tricel.InstId, 2);
                        if (listaCalendario != null && listaCalendario.Count > 0)
                        {

                            listaCalendario = listaCalendario.FindAll(p => p.Status == tricel.Id);
                            if (listaCalendario != null && listaCalendario.Count > 0)
                            {
                                calendario = listaCalendario[0];
                            }
                        }
                        string[] fechaIniArr = tricel.FechaInicio.Split('-');
                        DateTime fechaIni = new DateTime(
                            int.Parse(fechaIniArr[2]),
                            int.Parse(fechaIniArr[1]),
                            int.Parse(fechaIniArr[0]),
                            0, 0, 0
                            );
                        string[] fechaTerArr = tricel.FechaTermino.Split('-');
                        DateTime fechaTer = new DateTime(
                            int.Parse(fechaTerArr[2]),
                            int.Parse(fechaTerArr[1]),
                            int.Parse(fechaTerArr[0]),
                            23, 59, 0
                            );
                        //seteamos los valores
                        calendario.Titulo = tricel.Nombre;
                        calendario.Detalle = tricel.Nombre;
                        calendario.Asunto = tricel.Nombre;
                        calendario.Url = "";
                        calendario.Ubicacion = "CPAS";
                        calendario.InstId = tricel.InstId;
                        calendario.Etiqueta = 1;
                        calendario.Descripcion = tricel.Nombre;
                        calendario.Status = tricel.Id;
                        calendario.Tipo = 2;
                        calendario.FechaInicio = fechaIni;
                        calendario.FechaTermino = fechaTer;
                        calendario.UsuIdCreador = tricel.UsuIdCreador;
                        if (calendario.Id > 0)
                        {
                            //MODIFICAR
                            VCFramework.NegocioMySQL.Calendario.Modificar(calendario);
                        }
                        else
                        {
                            VCFramework.NegocioMySQL.Calendario.Insertar(calendario);
                        }

                        #endregion



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

                            //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, true);
                            MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeProyecto(institucion.Id, institucion.Nombre, tricel.Nombre, listaCorreos, true, false, false, esCpas);
                            //cr.Enviar(mnsj);
                            var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                            }
                    }
                }


                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(tricel);
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

            //validaciones antes de ejecutar la llamada.
            if (data.Id == 0)
                throw new ArgumentNullException("Id");

            string esCpasStr = "false";
            bool esCpas = false;
            if (data.EsCpas != null)
            {
                esCpasStr = data.EsCpas;
                esCpas = Convert.ToBoolean(esCpasStr);
            }

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string id = data.Id;
                int idBuscar = int.Parse(id);


                VCFramework.Entidad.Proyectos inst = VCFramework.NegocioMySQL.Proyectos.ObtenerProyectosPorId(idBuscar)[0];

                if (inst != null && inst.Id > 0)
                {
                    inst.Eliminado = 1;


                    VCFramework.NegocioMySQL.Proyectos.Modificar(inst);
                    //archivos tricel
                    List<VCFramework.Entidad.ArchivosProyecto> archivos = VCFramework.NegocioMySQL.ArchivosProyecto.ObtenerArchivosPorProyectoId(idBuscar, null);
                    if (archivos != null && archivos.Count > 0)
                    {
                        foreach (VCFramework.Entidad.ArchivosProyecto arc in archivos)
                        {
                            VCFramework.NegocioMySQL.ArchivosProyecto.Eliminar(arc);
                        }
                    }

                    #region calendario
                    VCFramework.Entidad.Calendario calendario = new VCFramework.Entidad.Calendario();
                    List<VCFramework.Entidad.Calendario> listaCalendario = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstidTipo(inst.InstId, 2);
                    if (listaCalendario != null && listaCalendario.Count > 0)
                    {

                        listaCalendario = listaCalendario.FindAll(p => p.Status == inst.Id);
                        if (listaCalendario != null && listaCalendario.Count > 0)
                        {
                            calendario = listaCalendario[0];
                        }
                    }
                    if (calendario.Id > 0)
                    {
                        calendario.Eliminado = 1;
                        VCFramework.NegocioMySQL.Calendario.Modificar(calendario);
                    }
                    #endregion

                    List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(inst.InstId);
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
                        VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(inst.InstId);
                        VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                        //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, true);
                        MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeProyecto(institucion.Id, institucion.Nombre, inst.Nombre, listaCorreos, false, false, true, esCpas);
                        //cr.Enviar(mnsj);
                        
                        var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                    }

                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(inst);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);

                }


            }
            catch (Exception ex)
            {
                httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                throw ex;
            }
            return httpResponse;

        }
    }
}