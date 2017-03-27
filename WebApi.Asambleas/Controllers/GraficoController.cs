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

namespace WebApi.Asambleas.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GraficoController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string nombreGrafico = "";
            //validaciones antes de ejecutar la llamada.
            if (data.NombreGrafico == "")
                throw new ArgumentNullException("NombreGrafico");
            else
                nombreGrafico = data.NombreGrafico;

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string instId = data.InstId;
                int instIdBuscar = int.Parse(instId);
                List<VCFramework.Entidad.Grafico> retorno = new List<Grafico>();

                if (nombreGrafico == "INGRESOS_EGRESOS")
                {
                    List<VCFramework.EntidadFuniconal.IngresoEgresoFuncional> graficos = VCFramework.NegocioMySQL.IngresoEgreso.ObtenerIngresoEgresoPorInstId(instIdBuscar);

                    if (graficos != null && graficos.Count > 0)
                    {
                        int totalIngresos = graficos.FindAll(p=>p.TipoMovimiento == 1).Sum(p => p.Monto);
                        int totalEgresos = graficos.FindAll(p => p.TipoMovimiento == 2).Sum(p => p.Monto);
                        VCFramework.Entidad.Grafico grIngreso = new Grafico();
                        VCFramework.Entidad.Grafico grEgreso = new Grafico();
                        grIngreso.value = totalIngresos;
                        grIngreso.label = "Ingresos";
                        retorno.Add(grIngreso);
                        grEgreso.value = totalEgresos;
                        grEgreso.label = "Egresos";
                        retorno.Add(grEgreso);

                    }



                }
                if (nombreGrafico == "PROYECTOS")
                {

                    //EN ESTE GRAFICO EL INSTID ES EL PROYECTO ID
                    List<VCFramework.Entidad.Votaciones> graficos = VCFramework.NegocioMySQL.Votaciones.ObtenerVotaciones(instIdBuscar);
                    if (graficos != null && graficos.Count > 0)
                    {
                        int totalIngresos = graficos.FindAll(p => p.Valor == 1).Count;
                        int totalEgresos = graficos.FindAll(p => p.Valor == 0).Count;
                        VCFramework.Entidad.Grafico grIngreso = new Grafico();
                        VCFramework.Entidad.Grafico grEgreso = new Grafico();
                        grIngreso.value = totalIngresos;
                        grIngreso.label = "Sí";
                        retorno.Add(grIngreso);
                        grEgreso.value = totalEgresos;
                        grEgreso.label = "No";
                        retorno.Add(grEgreso);
                    }
                    else
                    {
                        VCFramework.Entidad.Grafico grSi = new Grafico();
                        VCFramework.Entidad.Grafico grNo = new Grafico();
                        grSi.value = 0;
                        grSi.label = "Sí";
                        retorno.Add(grSi);
                        grNo.value = 0;
                        grNo.label = "No";
                        retorno.Add(grNo);
                    }



                }
                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(retorno);
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