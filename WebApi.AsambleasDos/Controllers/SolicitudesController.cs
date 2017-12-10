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
    public class SolicitudesController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [System.Web.Http.AcceptVerbs("PUT")]
        public HttpResponseMessage Put(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass, Newtonsoft.Json.Formatting.Indented);
            List<VCFramework.Entidad.UsuRol> lista = JsonConvert.DeserializeObject<List<VCFramework.Entidad.UsuRol>>(Input);

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {

                if (lista != null && lista.Count > 0)
                {
                    int instId = lista[0].InstId;
                    //si existen elementos asociados hay que eliminarlos todos y luego volver a crearlos
                    List<VCFramework.Entidad.UsuRol> rls = VCFramework.NegocioMySql.RlRolUsu.ObtenerPorInstId(instId);
                    if (rls != null && rls.Count > 0)
                    {
                        foreach(VCFramework.Entidad.UsuRol rl in rls)
                        {
                            VCFramework.NegocioMySql.RlRolUsu.Eliminar(rl);
                        }
                    }
                    //ahora que estan eliminados los volvemos a crear
                    foreach(VCFramework.Entidad.UsuRol rll in lista)
                    {
                        VCFramework.NegocioMySql.RlRolUsu.Insertar(rll);
                    }

                }

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

                List<VCFramework.Entidad.UsuRol> encabezado = VCFramework.NegocioMySql.RlRolUsu.ObtenerPorInstId(instIdBuscar);

                if (encabezado != null && encabezado.Count > 0)
                {
                    foreach (VCFramework.Entidad.UsuRol insti in encabezado)
                    {
                        VCFramework.Entidad.RolInstitucion rol = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolPorId(insti.RolId);
                        VCFramework.Entidad.Persona persona = VCFramework.NegocioMySQL.Persona.ObtenerPersonaPorId(insti.UsuId);

                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();

                        us.Id = insti.Id;
                        us.NombreCompleto = persona.Nombres + ' ' + persona.ApellidoPaterno + ' ' + persona.ApellidoMaterno;

                        us.NombreUsuario = rol.Nombre;
                        
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

    }
}