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
    public class RespuestaMuroController : ApiController
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

            VCFramework.Entidad.RespuestaMuro entidad = new RespuestaMuro();


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                //string instId = data.InstId;
                int idBuscar = int.Parse(id);

                VCFramework.Entidad.RespuestaMuro muro = VCFramework.NegocioMySql.RespuestaMuro.ObtenerRespuestaMuroPorId(idBuscar);

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

            List<VCFramework.Entidad.RespuestaMuro> lista = new List<RespuestaMuro>();

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                int idBuscar = int.Parse(mroId);

                List<VCFramework.Entidad.RespuestaMuro> muro = VCFramework.NegocioMySql.RespuestaMuro.ObtenerRespuestaMuroPorMuroId(idBuscar);
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
            //Request.RequestUri.Query.Split(new Char[] {'?', '&'})
            string idElementoEliminar = "0";
            //ahora manejamos si bienen elementos en el request
            if (Request.RequestUri != null)
            {
                if (Request.RequestUri.Query != null && Request.RequestUri.Query.Length > 0)
                {
                    string[] element = Request.RequestUri.Query.Split(new Char[] { '?', '&' });
                    if (element != null && element.Length > 0)
                    {
                        //1 = id, 2= escpas
                        string[] parts = element[1].Split('=');
                        if (parts.Length == 2)
                        {
                            idElementoEliminar = parts[1];
                        }
                    }
                }
            }
            if (DynamicClass != null)
            {

                string Input = JsonConvert.SerializeObject(DynamicClass);

                dynamic data = JObject.Parse(Input);

                //validaciones antes de ejecutar la llamada.
                if (data.Id == 0)
                    throw new ArgumentNullException("Id");

                idElementoEliminar = data.Id;
            }

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string idInstitucion = idElementoEliminar;
                int idInstitucionBuscar = int.Parse(idInstitucion);
                VCFramework.Entidad.RespuestaMuro inst = VCFramework.NegocioMySql.RespuestaMuro.ObtenerRespuestaMuroPorId(idInstitucionBuscar);

                if (inst != null && inst.Id > 0)
                {
                    inst.Eliminado = 1;

                    VCFramework.NegocioMySql.RespuestaMuro.Eliminar(inst);

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
            VCFramework.Entidad.RespuestaMuro aus = VCFramework.NegocioMySql.RespuestaMuro.ObtenerRespuestaMuroPorId(idInstitucionBuscar);

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
                    aus = new VCFramework.Entidad.RespuestaMuro();

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
                    {
                        nuevoId = VCFramework.NegocioMySql.RespuestaMuro.Insertar(aus);
                        aus.Id = nuevoId;
                    }
                    else
                    {
                        nuevoId = aus.Id;
                        VCFramework.NegocioMySql.RespuestaMuro.Modificar(aus);
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