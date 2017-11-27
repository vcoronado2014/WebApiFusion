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
    public class EncabezadoCargaController : ApiController
    {
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
                VCFramework.EntidadFuncional.proposalss logs = new VCFramework.EntidadFuncional.proposalss();
                logs.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.EncabezadoCarga> encabezado = VCFramework.NegocioMySql.EncabezadoCarga.ListarPorInstitucion(instIdBuscar);

                if (encabezado != null && encabezado.Count > 0)
                {
                    foreach (VCFramework.Entidad.EncabezadoCarga insti in encabezado)
                    {
                        VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(instIdBuscar);
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = insti.Id;
                        us.NombreCompleto = insti.NombreArchivo;
                        us.NombreUsuario = Utiles.ConstruyeFecha(insti.Fecha);
                        
                        if (insti.FilasCorrectas < 0)
                            insti.FilasCorrectas = 0;

                        us.OtroUno = insti.FilasCorrectas.ToString();
                        us.OtroDos = insti.FilasError.ToString();
                        us.OtroTres = insti.Resumen;
                        int totalFilas = insti.FilasCorrectas + insti.FilasError;
                        us.OtroCuatro = totalFilas.ToString();
                        us.OtroCinco = "modal_" + insti.Id.ToString();
                        if (institucion != null && institucion.Id > 0)
                        {
                            us.OtroSiete = institucion.Nombre;
                        }
                        logs.proposals.Add(us);
                    }
                    //establecimientos.Establecimientos = instituciones;
                }


                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(logs);
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

        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]string id)
        {

            if (id == "")
                throw new ArgumentNullException("Id");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                VCFramework.Entidad.EncabezadoCarga entidad = VCFramework.NegocioMySql.EncabezadoCarga.Obtener(int.Parse(id));

                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(entidad);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);

            }
            catch (Exception ex)
            {
                VCFramework.NegocioMySQL.Utiles.Log(ex);
                httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                throw ex;
            }
            return httpResponse;
        }
    }
}