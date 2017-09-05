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
    public class PermisoRolController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [AgendaExceptionFilter]
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]string instId, [FromUri]string rolId)
        {

            if (instId == "")
                throw new ArgumentNullException("InstitucionId");
            if (rolId == "")
                throw new ArgumentNullException("rolId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                List<VCFramework.Entidad.PermisoRol> permisos = new List<PermisoRol>();
                //string instId = data.InstId;
                int idInstitucion = int.Parse(instId);
                int rolIdBuscar = int.Parse(rolId);
                //evaluamos el rol
                permisos = VCFramework.NegocioMySql.PermisoRol.ObtenerPermisoRolPorRolId(rolIdBuscar, idInstitucion);

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(permisos);
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
        public HttpResponseMessage Get([FromUri]string instId)
        {

            if (instId == "")
                throw new ArgumentNullException("InstitucionId");

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                List<VCFramework.Entidad.PermisoRol> permisos = new List<PermisoRol>();
                //string instId = data.InstId;
                int idInstitucion = int.Parse(instId);
                //evaluamos el rol
                permisos = VCFramework.NegocioMySql.PermisoRol.ObtenerPermisoRolPorInstId(idInstitucion);

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(permisos);
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
            if (data.Id == 0)
                throw new ArgumentNullException("Id");

            VCFramework.Entidad.PermisoRol permiso = new PermisoRol();

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string id = data.Id;
                int idBuscar = int.Parse(id);

                permiso = VCFramework.NegocioMySql.PermisoRol.ObtenerPermisoRolPorId(idBuscar);

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(permiso);
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
            string instId = data.InstId;
            string rolId = data.RoldId;
            string creaUsuario = data.CreaUsuario;
            string modificaUsuario = data.ModificaUsuario;
            string eliminaUsuario = data.EliminaUsuario;
            string verUsuario = data.VerUsuario;
            string creaInstitucion = data.CreaInstitucion;
            string modificaInstitucion = data.ModificaInstitucion;
            string eliminaInstitucion = data.EliminaInstitucion;
            string verInstitucion = data.VerInstitucion;
            string creaDocumento = data.CreaDocumento;
            string eliminaDocumento = data.EliminaDocumento;
            string verDocumento = data.VerDocumento;
            string creaCalendario = data.CreaCalendario;
            string modificaCalendario = data.ModificaCalendario;
            string eliminaCalendario = data.EliminaCalendario;
            string verCalendario = data.VerCalendario;
            string creaTricel = data.CreaTricel;
            string modificaTricel = data.ModificaTricel;
            string eliminaTricel = data.EliminaTricel;
            string verTricel = data.VerTricel;
            string creaProyecto = data.CreaProyecto;
            string modificaProyecto = data.ModificaProyecto;
            string eliminaProyecto = data.EliminaProyecto;
            string verProyecto = data.VerProyecto;
            string creaRendicion = data.CreaRendicion;
            string modificaRendicion = data.ModificaRendicion;
            string eliminaRendicion = data.EliminaRendicion;
            string verRendicion = data.VerRendicion;
            string creaRol = data.CreaRol;
            string modificaRol = data.ModificaRol;
            string eliminaRol = data.EliminaRol;
            string verRol = data.VerRol;
            string creaMuro = data.CreaMuro;
            string modificaMuro = data.ModificaMuro;
            string eliminaMuro = data.EliminaMuro;
            string verMuro = data.VerMuro;
            string puedeVotarProyecto = data.PuedeVotarProyecto;
            string puedeVotarTricel = data.PuedeVotarTricel;

            int idNuevo = 0;

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                VCFramework.Entidad.PermisoRol permiso = new PermisoRol();
                //entonces vemos si modifica o crea
                
                permiso.Id = int.Parse(id);
                permiso.InstId = int.Parse(instId);
                permiso.CreaCalendario = int.Parse(creaCalendario);
                permiso.CreaDocumento = int.Parse(creaDocumento);
                permiso.CreaInstitucion = int.Parse(creaInstitucion);
                permiso.CreaMuro = int.Parse(creaMuro);
                permiso.CreaProyecto = int.Parse(creaProyecto);
                permiso.CreaRendicion = int.Parse(creaRendicion);
                permiso.CreaRol = int.Parse(creaRol);
                permiso.CreaTricel = int.Parse(creaTricel);
                permiso.CreaUsuario = int.Parse(creaUsuario);
                permiso.EliminaCalendario = int.Parse(eliminaCalendario);
                permiso.EliminaDocumento = int.Parse(eliminaDocumento);
                permiso.EliminaInstitucion = int.Parse(eliminaInstitucion);
                permiso.EliminaMuro = int.Parse(eliminaMuro);
                permiso.EliminaProyecto = int.Parse(eliminaProyecto);
                permiso.EliminaRendicion = int.Parse(eliminaRendicion);
                permiso.EliminaRol = int.Parse(eliminaRol);
                permiso.EliminaTricel = int.Parse(eliminaTricel);
                permiso.EliminaUsuario = int.Parse(eliminaUsuario);
                permiso.ModificaCalendario = int.Parse(modificaCalendario);
                permiso.ModificaInstitucion = int.Parse(modificaInstitucion);
                permiso.ModificaMuro = int.Parse(modificaMuro);
                permiso.ModificaProyecto = int.Parse(modificaProyecto);
                permiso.ModificaRendicion = int.Parse(modificaRendicion);
                permiso.ModificaRol = int.Parse(modificaRol);
                permiso.ModificaTricel = int.Parse(modificaTricel);
                permiso.ModificaUsuario = int.Parse(modificaUsuario);
                permiso.PuedeVotarProyecto = int.Parse(puedeVotarProyecto);
                permiso.PuedeVotarTricel = int.Parse(puedeVotarTricel);
                permiso.RolId = int.Parse(rolId);
                permiso.VerCalendario = int.Parse(verCalendario);
                permiso.VerDocumento = int.Parse(verDocumento);
                permiso.VerInstitucion = int.Parse(verInstitucion);
                permiso.VerMuro = int.Parse(verMuro);
                permiso.VerProyecto = int.Parse(verProyecto);
                permiso.VerRendicion = int.Parse(verRendicion);
                permiso.VerRol = int.Parse(verRol);
                permiso.VerTricel = int.Parse(verTricel);
                permiso.VerUsuario = int.Parse(verUsuario);



                if (int.Parse(id) > 0)
                {
                    VCFramework.NegocioMySql.PermisoRol.Modificar(permiso);
                }
                else
                {
                    idNuevo = VCFramework.NegocioMySql.PermisoRol.Insertar(permiso);
                    permiso.Id = idNuevo;
                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(permiso);
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

            string idElemento = data.Id;
            if (idElemento == null)
                throw new ArgumentNullException("Id");


            HttpResponseMessage httpResponse = new HttpResponseMessage();


            try
            {
                VCFramework.Entidad.PermisoRol permiso = VCFramework.NegocioMySql.PermisoRol.ObtenerPermisoRolPorId(int.Parse(idElemento));
                if (permiso != null && permiso.Id > 0)
                {
                    VCFramework.NegocioMySql.PermisoRol.Eliminar(permiso);
                }


                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(permiso);
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