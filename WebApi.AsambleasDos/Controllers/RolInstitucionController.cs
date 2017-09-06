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

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RolInstitucionController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (data.InstId == 0)
                throw new ArgumentNullException("InstId");

            List<VCFramework.Entidad.RolInstitucion> lista = new List<VCFramework.Entidad.RolInstitucion>();

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            String JSON = "";
            try
            {
                string idInstitucion = data.InstId;
                int idInstitucionBuscar = int.Parse(idInstitucion);
                List<VCFramework.Entidad.RolInstitucion> rolesInst = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolesPorInstId(idInstitucionBuscar);

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                JSON = JsonConvert.SerializeObject(rolesInst);
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

        [AgendaExceptionFilter]
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]string id)
        {
            VCFramework.Entidad.RolInstitucion retoorno = new VCFramework.Entidad.RolInstitucion();
            if (id == "")
                throw new ArgumentNullException("Id");

            //si trae un rol de superadministrador debería listar todos los usuarios
            int idElemento = int.Parse(id);

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                retoorno = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolPorId(idElemento);

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(retoorno);
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