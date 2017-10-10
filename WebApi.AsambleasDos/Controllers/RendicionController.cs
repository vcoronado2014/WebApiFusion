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
using System.Globalization;
using System.Net.Mail;

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RendicionController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [AgendaExceptionFilter]
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]string id)
        {

            //validaciones antes de ejecutar la llamada.
            if (id == "")
                throw new ArgumentNullException("Id");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                //string instId = data.InstId;
                int idBuscar = int.Parse(id);

                VCFramework.Entidad.IngresoEgreso ingresoEgreso = VCFramework.NegocioMySQL.IngresoEgreso.ObtenerIngresoEgresoPorId(idBuscar);


                if (ingresoEgreso != null)
                {

                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(ingresoEgreso);
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

        [System.Web.Http.AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (data.Id == 0)
                throw new ArgumentNullException("Id");

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string id = data.Id;
                int idBuscar = int.Parse(id);


                VCFramework.Entidad.IngresoEgreso inst = VCFramework.NegocioMySQL.IngresoEgreso.ObtenerPorId(idBuscar);

                if (inst != null && inst.Id > 0)
                {
                    inst.Eliminado = 1;
                    string tipoMov = "Ingreso";
                    if (inst.TipoMovimiento == 2)
                        tipoMov = "Egreso";


                    VCFramework.NegocioMySQL.IngresoEgreso.Modificar(inst);

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

                        //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, false);
                        MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeRendicion(institucion.Id, institucion.Nombre, tipoMov, inst.Monto.ToString(), listaCorreos, false, false, true);

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
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);


            string buscarNombreUsuario = "";
            if (data.BuscarId != null)
            {
                buscarNombreUsuario = data.BuscarId;
            }

            //validaciones antes de ejecutar la llamada.
            if (data.InstId == 0)
                throw new ArgumentNullException("InstId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string instId = data.InstId;
                int instIdBuscar = int.Parse(instId);

                VCFramework.EntidadFuncional.proposalss rendiciones = new VCFramework.EntidadFuncional.proposalss();
                rendiciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.EntidadFuniconal.IngresoEgresoFuncional> instituciones = VCFramework.NegocioMySQL.IngresoEgreso.ObtenerIngresoEgresoPorInstId(instIdBuscar);

                if (buscarNombreUsuario != "")
                {
                    if (VCFramework.NegocioMySQL.Utiles.IsNumeric(buscarNombreUsuario))
                        instituciones = instituciones.FindAll(p => p.Id == int.Parse(buscarNombreUsuario));
                    else
                        instituciones = instituciones.FindAll(p => p.Detalle.ToUpper().Replace(" ", "") == buscarNombreUsuario.ToUpper().Replace(" ", ""));
                }
                if (instituciones != null && instituciones.Count > 0)
                {
                    foreach (VCFramework.EntidadFuniconal.IngresoEgresoFuncional insti in instituciones)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = insti.Id;
                        us.NombreCompleto = insti.Detalle;
                        us.NombreUsuario = insti.FechaMovimiento.ToShortDateString().Replace('/', '-');
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
                        us.OtroCinco = insti.TipoMovimiento.ToString();

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

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(rendiciones);
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
            return new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
        }
    }
}