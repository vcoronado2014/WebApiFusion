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

namespace WebApi.Asambleas.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FileController : ApiController
    {
        const string UploadDirectory = "~/Repositorio/";
        const string UploadDirectoryImg = "~/img/";
        [HttpPost]
        public HttpResponseMessage UploadFile()
        {
            //if (HttpContext.Current.Request.Files.AllKeys.Any())
            //{
            // Get the uploaded image from the Files collection
            var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
            string input = HttpContext.Current.Request.Form["rendicion"];

            string[] inputs = input.Split(',');
            //string Input = JsonConvert.SerializeObject(ppp);

            dynamic data = JObject.Parse(input);

            string id = data.Id;
            if (id == null)
                id = "0";
            int idBuscar = int.Parse(id);

            //validaciones antes de ejecutar la llamada.
            VCFramework.EntidadFuniconal.IngresoEgresoFuncional aus = VCFramework.NegocioMySQL.IngresoEgreso.ObtenerIngresoEgresoPorId(idBuscar);
            VCFramework.Entidad.IngresoEgreso entidad = new VCFramework.Entidad.IngresoEgreso();
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string detalle = data.Detalle;
                string numderoComprobante = data.NumeroComprobante;
                string monto = data.Monto;
                string tipoMovimiento = data.IdTipoMovimiento;
                string idUsuario = data.IdUsuario;
                string instId = data.InstId;
                string nombreArchivo = data.NombreArchivo;
                string archivoGuardar = "";
                if (httpPostedFile != null && httpPostedFile.FileName != null)
                {
                    archivoGuardar = httpPostedFile.FileName;
                }
                else
                {
                    archivoGuardar = "#";
                }
                //quitar //
                string[] pedazos = nombreArchivo.Split('\\');
                string nom = pedazos[pedazos.Length - 1].ToString();

                if (aus == null)
                    aus = new VCFramework.EntidadFuniconal.IngresoEgresoFuncional();

                if (aus != null)
                {

                    int nuevoId = 0;

                    if (aus.Id == 0)
                    {
                        entidad.Detalle = detalle;
                    }
                    entidad.Detalle = detalle;
                    entidad.FechaMovimiento = DateTime.Now;
                    entidad.Eliminado = 0;
                    entidad.InstId = int.Parse(instId);
                    entidad.Monto = int.Parse(monto);
                    entidad.NumeroComprobante = numderoComprobante;
                    entidad.TipoMovimiento = int.Parse(tipoMovimiento);
                    entidad.UrlDocumento = archivoGuardar;
                    entidad.UsuId = int.Parse(idUsuario);

                    if (aus.Id == 0)
                    {
                        nuevoId = VCFramework.NegocioMySQL.IngresoEgreso.Insertar(entidad);
                        if (nuevoId >= 0)
                        {
                            if (VCFramework.NegocioMySQL.Utiles.ENVIA_RENDICIONES(int.Parse(instId)) == "1")
                            {

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

                                    MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearRendicion(institucion.Nombre, entidad.Detalle, listaCorreos, true);
                                    //cr.Enviar(mnsj);
                                    var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                                }

                            }
                        }
                    }
                    else
                    {
                        nuevoId = aus.Id;
                        entidad.Id = nuevoId;
                        VCFramework.NegocioMySQL.IngresoEgreso.Modificar(entidad);
                        if (VCFramework.NegocioMySQL.Utiles.ENVIA_RENDICIONES(int.Parse(instId)) == "1")
                        {

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

                                MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearRendicion(institucion.Nombre, entidad.Detalle, listaCorreos, false);
                                //cr.Enviar(mnsj);
                                var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                            }

                        }
                    }


                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(entidad);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);


                }
            }
            catch (Exception ex)
            {
                httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                throw ex;
            }


            if (httpPostedFile != null)
            {

                // Validate the uploaded image(optional)

                // Get the complete file path
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Repositorio"), httpPostedFile.FileName);

                // Save the uploaded file to "UploadedFiles" folder
                httpPostedFile.SaveAs(fileSavePath);
            }

            return httpResponse;
        }
        
    }
}