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

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ArchivoController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {
            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            if (data.InstId == "")
                throw new ArgumentNullException("InstId");
            if (data.ElementoId == "")
                throw new ArgumentNullException("ElementoId");
            if (data.TipoPadre == "")
                throw new ArgumentNullException("TipoPadre");

            string instId = data.InstId;
            string elementoId = data.ElementoId;
            string tipoPadre = data.TipoPadre;


            HttpResponseMessage httpResponse = new HttpResponseMessage();


            try
            {

                List<VCFramework.Entidad.ArchivoAdjunto> archivos = VCFramework.NegocioMySql.ArchivoAdjunto.Listar(int.Parse(instId), int.Parse(elementoId), int.Parse(tipoPadre));

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