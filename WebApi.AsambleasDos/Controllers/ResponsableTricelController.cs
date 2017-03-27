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
    public class ResponsableTricelController : ApiController
    {

        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

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
                List<VCFramework.Entidad.ResponsableTricelEnvoltorio> listaDevolver = new List<ResponsableTricelEnvoltorio>();

                List<VCFramework.Entidad.AutentificacionUsuario> usuarios = VCFramework.NegocioMySQL.AutentificacionUsuario.ListarUsuariosPorInstId(instIdBuscar);

                if (usuarios != null && usuarios.Count > 0)
                {
                    foreach(VCFramework.Entidad.AutentificacionUsuario usu in usuarios)
                    {
                        VCFramework.Entidad.ResponsableTricelEnvoltorio entidad = new ResponsableTricelEnvoltorio();

                        VCFramework.Entidad.Persona persona =  VCFramework.NegocioMySQL.Persona.ObtenerPersonaPorUsuId(usu.Id);

                        entidad.AusId = usu.Id;
                        entidad.InstId = usu.InstId;
                        entidad.Nombre = persona.Nombres + " " + persona.ApellidoPaterno + " " + persona.ApellidoMaterno;
                        listaDevolver.Add(entidad);
                    }
                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(listaDevolver);
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