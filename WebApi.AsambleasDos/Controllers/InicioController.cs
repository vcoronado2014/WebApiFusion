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
using System.Globalization;
using System.Text;
using System.IO;

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InicioController : ApiController
    {
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (data.InstId == 0)
                throw new ArgumentNullException("InstitucionId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string instId = data.InstId;
                string usuId = data.UsuId;
                string rolId = data.RolId;
                
                string tipo = data.Tipo;
                int idInstitucion = int.Parse(instId);
                int idUsuario = int.Parse(usuId);
                int idRol = int.Parse(rolId);

                VCFramework.EntidadFuncional.Inicio inicio = new VCFramework.EntidadFuncional.Inicio();

                #region eventos
                List<VCFramework.Entidad.Calendario> eventos = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstId(idInstitucion);

                List<VCFramework.EntidadFuncional.evento> listaEventos = new List<VCFramework.EntidadFuncional.evento>();
                if (eventos != null && eventos.Count > 0)
                {


                    foreach (VCFramework.Entidad.Calendario cal in eventos)
                    {
                        VCFramework.EntidadFuncional.evento entidad = new VCFramework.EntidadFuncional.evento();
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

                        entidad.id = cal.Id;
                        entidad.clientId = entidad.id;
                        listaEventos.Add(entidad);
                    }

                }

                if (listaEventos != null && listaEventos.Count > 0)
                {
                    inicio.Eventos = new List<VCFramework.EntidadFuncional.evento>();
                    inicio.Eventos = listaEventos;

                }
                #endregion

                #region Proyectos
                VCFramework.EntidadFuncional.proposalss votaciones = new VCFramework.EntidadFuncional.proposalss();
                votaciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.Proyectos> triceles = VCFramework.NegocioMySQL.Proyectos.ObtenerProyectosPorInstIdTodos(idInstitucion);
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

                        //verificamos si ya votó
                        StringBuilder sb = new StringBuilder();
                        if (usuId != "0")
                        {
                            List<VCFramework.Entidad.Votaciones> listaVotaciones = VCFramework.NegocioMySQL.Votaciones.ObtenerVotaciones(tri.Id, int.Parse(usuId));
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


                        votaciones.proposals.Add(us);
                    }
                    //establecimientos.Establecimientos = instituciones;
                }

                if (votaciones.proposals != null && votaciones.proposals.Count > 0)
                {
                    inicio.Proyectos = new VCFramework.EntidadFuncional.proposalss();
                    inicio.Proyectos.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();
                    inicio.Proyectos.proposals = votaciones.proposals;
                }
                #endregion

                #region Triceles
                VCFramework.EntidadFuncional.proposalss votaciones2 = new VCFramework.EntidadFuncional.proposalss();
                votaciones2.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.Tricel> triceles2 = VCFramework.NegocioMySQL.Tricel.ObtenerTricelPorInstIdTodos(idInstitucion);

                StringBuilder sbTextoVoto = new StringBuilder();

                if (triceles2 != null && triceles2.Count > 0)
                {
                    foreach (VCFramework.Entidad.Tricel tri in triceles2)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = tri.Id;
                        us.NombreCompleto = tri.Objetivo;
                        us.NombreUsuario = tri.Nombre;

                        us.OtroUno = tri.FechaInicio;
                        us.OtroDos = tri.FechaTermino;
                        us.OtroTres = tri.FechaCreacion.ToShortDateString();

                        us.OtroCinco = us.OtroUno + " - " + us.OtroDos;
                        List<VCFramework.Entidad.ResponsableTricel> responsables = VCFramework.NegocioMySQL.ResponsableTricel.ObtenerResponsables(tri.Id);
                        if (responsables != null && responsables.Count > 0)
                            us.OtroOcho = responsables[0].UsuId.ToString();
                        else
                            us.OtroOcho = "0";

                        us.OtroNueve = sbTextoVoto.ToString();

                        votaciones2.proposals.Add(us);
                    }
                    //establecimientos.Establecimientos = instituciones;
                }

                if (votaciones2.proposals != null && votaciones2.proposals.Count > 0)
                {
                    inicio.Votaciones = new VCFramework.EntidadFuncional.proposalss();
                    inicio.Votaciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();
                    inicio.Votaciones.proposals = votaciones2.proposals;
                }
                #endregion

                #region usuarios
                List<VCFramework.EntidadFuncional.UsuarioEnvoltorio> usuarios = VCFramework.NegocioMySQL.AutentificacionUsuario.ListarUsuariosEnvoltorioPorRol(idInstitucion, idRol);
                if (usuarios != null && usuarios.Count > 0)
                {
                    inicio.Usuarios = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();
                    inicio.Usuarios = usuarios;

                }
                #endregion

                #region establecimientos
                VCFramework.EntidadFuncional.proposalss establecimientos = new VCFramework.EntidadFuncional.proposalss();
                establecimientos.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.Institucion> instituciones = VCFramework.NegocioMySQL.Institucion.ListarInstitucionesSinCache();

                if (instituciones != null && instituciones.Count > 0)
                {
                    foreach (VCFramework.Entidad.Institucion insti in instituciones)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = insti.Id;
                        us.NombreCompleto = insti.Nombre;
                        us.Url = insti.Url;
                        us.UrlEliminar = insti.UrlEliminar;
                        establecimientos.proposals.Add(us);
                    }
                    //establecimientos.Establecimientos = instituciones;
                }

                if (establecimientos.proposals != null && establecimientos.proposals.Count > 0)
                {
                    inicio.Establecimientos = new VCFramework.EntidadFuncional.proposalss();
                    inicio.Establecimientos.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();
                    inicio.Establecimientos.proposals = establecimientos.proposals;
                }
                #endregion

                #region rendiciones
                VCFramework.EntidadFuncional.proposalss rendiciones = new VCFramework.EntidadFuncional.proposalss();
                rendiciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.EntidadFuniconal.IngresoEgresoFuncional> listaRendiciones = VCFramework.NegocioMySQL.IngresoEgreso.ObtenerIngresoEgresoPorInstId(idInstitucion);

                if (listaRendiciones != null && listaRendiciones.Count > 0)
                {
                    foreach (VCFramework.EntidadFuniconal.IngresoEgresoFuncional insti in listaRendiciones)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = insti.Id;
                        us.NombreCompleto = insti.Detalle;
                        us.NombreUsuario = insti.FechaMovimiento.ToShortDateString();
                        us.OtroUno = insti.TipoMovimientoString;
                        us.OtroDos = insti.NumeroComprobante;
                        //us.OtroTres = insti.Monto.ToString();
                        System.Globalization.CultureInfo cultura = System.Globalization.CultureInfo.GetCultureInfo("es-CL");
                        NumberFormatInfo nfi = new CultureInfo("es-CL", false).NumberFormat;
                        nfi.CurrencyDecimalDigits = 0;

                        //cultura.NumberFormat.CurrencyDecimalDigits = 0;
                        decimal result = insti.Monto / 1m;
                        string moneda = result.ToString("c", nfi);

                        us.OtroTres = insti.Monto.ToString();
                        us.OtroCuatro = moneda;

                        us.UrlDocumento = insti.UrlDocumento;
                        us.Url = "CrearModificarIngresoEgreso.html?id=" + us.Id.ToString() + "&ELIMINAR=0";
                        us.UrlEliminar = "CrearModificarIngresoEgreso.html?id=" + us.Id.ToString() + "&ELIMINAR=1";
                        string urlll = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/Repositorio/";
                        if (us.UrlDocumento != null && us.UrlDocumento != "" && us.UrlDocumento != "#")
                        {
                            us.Rol = urlll + us.UrlDocumento;
                            us.MostrarItem1 = true;
                        }
                        else
                        {
                            us.Rol = "#";
                            us.MostrarItem1 = false;
                        }

                        rendiciones.proposals.Add(us);
                    }
                    //establecimientos.Establecimientos = instituciones;
                }

                if (rendiciones.proposals != null && rendiciones.proposals.Count > 0)
                {
                    inicio.Rendiciones = new VCFramework.EntidadFuncional.proposalss();
                    inicio.Rendiciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();
                    inicio.Rendiciones.proposals = rendiciones.proposals;
                }
                #endregion

                #region documentos
                List<VCFramework.Entidad.DocumentosUsuario> documentos = VCFramework.NegocioMySQL.DocumentosUsuario.ObtenerDocumentosPorInstIdNuevo(idInstitucion);
                VCFramework.EntidadFuncional.proposalss documentosE = new VCFramework.EntidadFuncional.proposalss();
                documentosE.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                if (documentos != null && documentos.Count > 0)
                {
                    foreach (VCFramework.Entidad.DocumentosUsuario doc in documentos)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio entidadS = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        entidadS.Id = doc.Id;
                        entidadS.NombreCompleto = doc.NombreArchivo;
                        entidadS.NombreUsuario = doc.FechaSubida;
                        entidadS.OtroUno = doc.Tamano.ToString() + " Kb";

                        string extension = Path.GetExtension(doc.NombreArchivo);
                        entidadS.OtroTres = extension;

                        //HttpContext.Current.Server.MapPath("~/Repositorio")
                        string urlll = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/Repositorio/";
                        if (doc.NombreArchivo != null && doc.NombreArchivo != "")
                            entidadS.Url = urlll + doc.NombreArchivo;
                        else
                            entidadS.Url = "#";

                        //vista previa del archivo
                        string urlVista = "http://docs.google.com/viewer?url=" + entidadS.Url + " &embedded=true";
                        entidadS.OtroDos = urlVista;


                        entidadS.UrlEliminar = "EliminarDocumento.html?id=" + entidadS.Id.ToString() + "&tipo=documentousuario";

                        documentosE.proposals.Add(entidadS);
                    }
                }

                if (documentosE.proposals != null && documentosE.proposals.Count > 0)
                {
                    inicio.Documentos = new VCFramework.EntidadFuncional.proposalss();
                    inicio.Documentos.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();
                    inicio.Documentos.proposals = documentosE.proposals;
                }
                #endregion


                if (inicio != null)
                {
                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(inicio);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);
                }
                else
                {
                    httpResponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                //Uri uri = new Uri(AgendaWeb.Integracion.Utils.ObtenerUrlLogin());

                //httpResponse = AgendaWeb.Integracion.PostResponse.GetResponse(uri, Input);
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