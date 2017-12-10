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
    public class ResSolMuroController : ApiController
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

            VCFramework.Entidad.ResSolmuro entidad = new ResSolmuro();


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                //string instId = data.InstId;
                int idBuscar = int.Parse(id);

                VCFramework.Entidad.ResSolmuro muro = VCFramework.NegocioMySql.ResSolmuro.ObtenerRespuestaMuroPorId(idBuscar);

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(entidad);
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

        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (data.MroId == 0)
                throw new ArgumentNullException("MroId");

            string mroId = data.MroId;

            List<VCFramework.Entidad.ResSolmuro> lista = new List<VCFramework.Entidad.ResSolmuro>();

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                int idBuscar = int.Parse(mroId);

                List<VCFramework.Entidad.ResSolmuro> muro = VCFramework.NegocioMySql.ResSolmuro.ObtenerRespuestaMuroPorMuroId(idBuscar);
                lista = muro;

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(lista);
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
                string idInstitucion = data.Id;
                int idInstitucionBuscar = int.Parse(idInstitucion);
                VCFramework.Entidad.ResSolmuro inst = VCFramework.NegocioMySql.ResSolmuro.ObtenerRespuestaMuroPorId(idInstitucionBuscar);

                if (inst != null && inst.Id > 0)
                {
                    inst.Eliminado = 1;

                    VCFramework.NegocioMySql.ResSolmuro.Modificar(inst);

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

        [System.Web.Http.AcceptVerbs("PUT")]
        public HttpResponseMessage Put(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string idInstitucion = data.Id;
            if (idInstitucion == null)
                idInstitucion = "0";
            int idInstitucionBuscar = int.Parse(idInstitucion);


            //validaciones antes de ejecutar la llamada.
            VCFramework.Entidad.ResSolmuro aus = VCFramework.NegocioMySql.ResSolmuro.ObtenerRespuestaMuroPorId(idInstitucionBuscar);

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string muroId = data.MroId;
                string instId = data.InstId;
                string usuId = data.UsuId;
                string prioridadId = data.PrioridadId;
                string rolId = data.RolId;
                string texto = data.Texto;


                if (aus == null)
                    aus = new VCFramework.Entidad.ResSolmuro();

                if (aus != null)
                {
                    int nuevoId = 0;

                    //prioridadId = 0 baja, 1 media, 2 alta
                    if (int.Parse(prioridadId) == 0)
                        aus.UrlImagen = "img/green.png";
                    if (int.Parse(prioridadId) == 1)
                        aus.UrlImagen = "img/blue.png";
                    if (int.Parse(prioridadId) == 2)
                        aus.UrlImagen = "img/red.png";

                    aus.MroId = int.Parse(muroId);
                    aus.FechaCreacion = DateTime.Now;
                    aus.InstId = int.Parse(instId);
                    aus.PrioridadId = int.Parse(prioridadId);
                    aus.RolId = int.Parse(rolId);
                    aus.Texto = texto;
                    aus.UsuId = int.Parse(usuId);

                    if (aus.Id == 0)
                        nuevoId = VCFramework.NegocioMySql.ResSolmuro.Insertar(aus);
                    else
                    {
                        nuevoId = aus.Id;
                        VCFramework.NegocioMySql.ResSolmuro.Modificar(aus);
                    }


                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(aus);
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
    }
}