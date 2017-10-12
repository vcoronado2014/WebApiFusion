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
    public class VotarTricelController : ApiController
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
            string ltrId = data.LtrId;
            
            string usuId = data.UsuId;
            string triId = data.TriId;

            string ticket = VCFramework.NegocioMySQL.Utiles.Encriptar(instId) + VCFramework.NegocioMySQL.Utiles.Encriptar(ltrId) + VCFramework.NegocioMySQL.Utiles.Encriptar(triId) + VCFramework.NegocioMySQL.Utiles.Encriptar(usuId);

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            int idNuevo = 0;



            try
            {
                VCFramework.Entidad.VotTricel votacion = new VCFramework.Entidad.VotTricel();
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");
                IFormatProvider culture1 = new System.Globalization.CultureInfo("es-CL", true);
                if (id == "0")
                {
                    votacion.Eliminado = 0;
                    votacion.FechaVotacion = DateTime.Now;
                    votacion.InstId = int.Parse(instId);
                    votacion.LtrId = int.Parse(ltrId);
                    votacion.UsuIdVotador = int.Parse(usuId);
                    votacion.TriId = int.Parse(triId);
                    
                    VCFramework.NegocioMySQL.VotTricel.Insertar(votacion);

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
            string ltrId = data.LtrId;

            string usuId = data.UsuId;

            string ticket = VCFramework.NegocioMySQL.Utiles.Encriptar(instId) + VCFramework.NegocioMySQL.Utiles.Encriptar(ltrId) + VCFramework.NegocioMySQL.Utiles.Encriptar(id) + VCFramework.NegocioMySQL.Utiles.Encriptar(usuId);

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            int idNuevo = 0;

            try
            {
                VCFramework.Entidad.VotTricel votacion = new VCFramework.Entidad.VotTricel();
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");
                IFormatProvider culture1 = new System.Globalization.CultureInfo("es-CL", true);
                if (id == "0")
                {
                    votacion.Eliminado = 0;
                    votacion.FechaVotacion = DateTime.Now;
                    votacion.InstId = int.Parse(instId);
                    votacion.LtrId = int.Parse(ltrId);
                    votacion.UsuIdVotador = int.Parse(usuId);

                    VCFramework.NegocioMySQL.VotTricel.Insertar(votacion);

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