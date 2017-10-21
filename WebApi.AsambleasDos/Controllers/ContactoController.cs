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

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContactoController : ApiController
    {
        [System.Web.Http.AcceptVerbs("PUT")]
        public HttpResponseMessage Put(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string para = data.Para;
            string mensaje = data.Mensaje;
            string nombre = data.Nombre;
            string telefono = data.Telefono;

            string esCpasStr = "false";
            bool esCpas = false;
            if (data.EsCpas != null)
            {
                esCpasStr = data.EsCpas;
                esCpas = Convert.ToBoolean(esCpasStr);
            }


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            MailMessage mnsj = null;

            try
            {
                List<string> listaCorreos = new List<string>();
                listaCorreos.Add(para);
                if (listaCorreos != null && listaCorreos.Count > 0)
                {
                    VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                    mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeContacto(nombre, telefono, para, mensaje, esCpas);
                    //cr.Enviar(mnsj);
                    var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                }


                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(mnsj);
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