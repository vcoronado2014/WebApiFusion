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
using System.Data.OleDb;


namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CargaMasivaController : ApiController
    {
        const string UploadDirectory = "~/Excel/";
        const string UploadDirectoryImg = "~/apps/img/";

        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);


           //validaciones antes de ejecutar la llamada.
            if (data.InstId == 0)
                throw new ArgumentNullException("InstId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string instId = data.InstId;
                int instIdBuscar = int.Parse(instId);

                VCFramework.EntidadFuncional.proposalss archivos = new VCFramework.EntidadFuncional.proposalss();
                archivos.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                //ac+á listar los archivos de la carpeta

                string rutaBuscar =  Path.Combine(HttpContext.Current.Server.MapPath("~/Excel"));
                DirectoryInfo di = new DirectoryInfo(rutaBuscar);
                int idP = 1;
                foreach(var fi in di.GetFiles())
                {
                    if (fi.Name.Length > 10)
                    {
                        string[] archivo = fi.Name.Split('_');
                        if (archivo.Length == 5)
                        {
                            string idInst = archivo[3];
                            //comparamos el id inst
                            if (idInst == instId)
                            {
                                string[] fechaExtension = archivo[4].Split('.');
                                if (fechaExtension.Length > 0)
                                {
                                    string fechaHoraEntera = fechaExtension[0];
                                    if (fechaHoraEntera.Length == 12)
                                    {
                                        //separamos
                                        string anno = fechaHoraEntera.Substring(0, 4);
                                        string mes = fechaHoraEntera.Substring(4, 2);
                                        string dia = fechaHoraEntera.Substring(6, 2);
                                        string hora = fechaHoraEntera.Substring(8, 2);
                                        string minutos = fechaHoraEntera.Substring(10, 2);
                                        //ahora construimos el retorno
                                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                                        us.Id = idP;
                                        us.NombreCompleto = fi.Name;
                                        us.NombreUsuario = dia + "-" + mes + "-" + anno + " " + hora + ":" + minutos;
                                        //us.OtroUno = insti.TipoMensaje.ToString();
                                        string urlll = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/Excel/";
                                        if (fi.Name != null && fi.Name != "")
                                            us.UrlDocumento = urlll + fi.Name;
                                        else
                                            us.UrlDocumento = "#";
                                        archivos.proposals.Add(us);
                                        idP++;

                                    }
                                }
                            }

                        }
                    }
                }
                

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(archivos);
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
}