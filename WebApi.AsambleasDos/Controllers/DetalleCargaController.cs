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
    public class DetalleCargaController : ApiController
    {
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);


            //validaciones antes de ejecutar la llamada.
            if (data.EcId == 0)
                throw new ArgumentNullException("EcId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string ecId = data.EcId;
                int ecIdBuscar = int.Parse(ecId);
                VCFramework.EntidadFuncional.proposalss logs = new VCFramework.EntidadFuncional.proposalss();
                logs.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.LogCarga> encabezado = VCFramework.NegocioMySql.LogCarga.ListarPorEncabezadoId(ecIdBuscar);

                if (encabezado != null && encabezado.Count > 0)
                {
                    foreach (VCFramework.Entidad.LogCarga insti in encabezado)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = insti.Id;
                        us.NombreCompleto = insti.NombreUsuario;
                        us.NombreUsuario = insti.NumeroFila.ToString();
                        if (insti.TipoMensaje == 0)
                            us.OtroUno = "Error";
                        else
                            us.OtroUno = "Correcto";

                        us.OtroDos = insti.Detalle;
                        logs.proposals.Add(us);
                    }

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
    }
}