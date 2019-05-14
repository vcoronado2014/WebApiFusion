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
    public class FirebaseController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [System.Web.Http.AcceptVerbs("PUT")]
        public HttpResponseMessage Put(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string token = data.Token;
            string instId = data.InstId;
            string usuId = data.UsuId;
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");
            //validaciones antes de ejecutar la llamada.
            //VCFramework.Entidad.Institucion aus = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorIdSinCache(idInstitucionBuscar);
            VCFramework.Entidad.TokenUsuario tok = VCFramework.NegocioMySql.TokenUsuario.ObtenerPorToken(token, int.Parse(instId));

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                //aca hay que guardar
                if (tok != null && tok.Id > 0)
                {
                    //ya existe, actualizar
                    //tok.FechaActualizacion = DateTime.Parse(DateTime.Now, culture);
                    tok.FechaActualizacion = DateTime.Now;
                    if (tok.UsuId == 0)
                    {
                        tok.UsuId = int.Parse(usuId);
                    }
                    if (tok.InstId == 0)
                    {
                        tok.InstId = int.Parse(instId);
                    }
                    VCFramework.NegocioMySql.TokenUsuario.Modificar(tok);

                }
                else
                {
                    //no existe, insertar
                    tok.FechaActualizacion = DateTime.Now;
                    tok.FechaCreacion = DateTime.Now;
                    tok.Token = token;
                    tok.InstId = int.Parse(instId);
                    tok.UsuId = int.Parse(usuId);
                    VCFramework.NegocioMySql.TokenUsuario.Insertar(tok);

                }
                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(tok);
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