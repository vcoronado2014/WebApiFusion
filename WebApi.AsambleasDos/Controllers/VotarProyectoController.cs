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
    public class VotarProyectoController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {
            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string id = data.Id;
            if (id == null)
                id = "0";

            string instId = data.InstId;
            string proId = data.ProId;
            string valor = data.Valor;
            string usuId = data.UsuId;

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            int idNuevo = 0;



            try
            {
                VCFramework.Entidad.Votaciones votacion = new VCFramework.Entidad.Votaciones();
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");
                IFormatProvider culture1 = new System.Globalization.CultureInfo("es-CL", true);
                if (id == "0")
                {
                    votacion.Eliminado = 0;
                    votacion.FechaVotacion = DateTime.Now;
                    votacion.InstId = int.Parse(instId);
                    votacion.ProId = int.Parse(proId);
                    votacion.UsuIdVotador = int.Parse(usuId);
                    VCFramework.NegocioMySQL.Votaciones.Insertar(votacion);

                }



                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(votacion);
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
            string proId = data.ProId;
            string valor = data.Valor;
            string usuId = data.UsuId;

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            int idNuevo = 0;



            try
            {
                VCFramework.Entidad.Votaciones votacion = new VCFramework.Entidad.Votaciones();
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");
                IFormatProvider culture1 = new System.Globalization.CultureInfo("es-CL", true);
                if (id == "0")
                {
                    votacion.Eliminado = 0;
                    votacion.FechaVotacion = DateTime.Now;
                    votacion.InstId = int.Parse(instId);
                    votacion.ProId = int.Parse(proId);
                    votacion.UsuIdVotador = int.Parse(usuId);
                    VCFramework.NegocioMySQL.Votaciones.Insertar(votacion);

                }



                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(votacion);
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