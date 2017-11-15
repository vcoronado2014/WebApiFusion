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
using System.ComponentModel;
using System.Globalization;

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MailingController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

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
                List<VCFramework.Entidad.Mailing> permisos = new List<Mailing>();
                //string instId = data.InstId;
                int idInstitucion = int.Parse(instId);
                  //evaluamos el rol
                permisos = VCFramework.NegocioMySql.Mailing.ObtenerMailingPorInstId(idInstitucion);

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

            VCFramework.Entidad.Mailing permiso = new Mailing();

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string id = data.Id;
                int idBuscar = int.Parse(id);

                permiso = VCFramework.NegocioMySql.Mailing.ObtenerMailingPorId(idBuscar);

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

        /// <summary>
        /// Insertar o Modifica Mailing
        /// </summary>
        /// <param name="DynamicClass">Entidad.Mailing</param>
        /// <returns>HttpResponseMessage</returns>
        [System.Web.Http.AcceptVerbs("PUT")]
        [Description("Inserta o modifica Mailing")]
        public HttpResponseMessage Put(dynamic DynamicClass)
        {
            

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string id = data.Id;
            string instId = data.InstId;
            string usuId = data.UsuId;
            string creaUsuario = data.CreaUsuario;
            string modificaUsuario = data.ModificaUsuario;
            string eliminaUsuario = data.EliminaUsuario;
            string creaInstitucion = data.CreaInstitucion;
            string modificaInstitucion = data.ModificaInstitucion;
            string eliminaInstitucion = data.EliminaInstitucion;
            string creaDocumento = data.CreaDocumento;
            string eliminaDocumento = data.EliminaDocumento;
            string creaCalendario = data.CreaCalendario;
            string modificaCalendario = data.ModificaCalendario;
            string eliminaCalendario = data.EliminaCalendario;
            string creaTricel = data.CreaTricel;
            string modificaTricel = data.ModificaTricel;
            string eliminaTricel = data.EliminaTricel;
            string creaProyecto = data.CreaProyecto;
            string modificaProyecto = data.ModificaProyecto;
            string eliminaProyecto = data.EliminaProyecto;
            string creaRendicion = data.CreaRendicion;
            string modificaRendicion = data.ModificaRendicion;
            string eliminaRendicion = data.EliminaRendicion;
            string creaRol = data.CreaRol;
            string modificaRol = data.ModificaRol;
            string eliminaRol = data.EliminaRol;
            string creaMuro = data.CreaMuro;
            string modificaMuro = data.ModificaMuro;
            string eliminaMuro = data.EliminaMuro;

            int idNuevo = 0;
            int idNuevoRol = 0;

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                VCFramework.Entidad.Mailing permiso = new Mailing();
                List<VCFramework.Entidad.Mailing> mailing = VCFramework.NegocioMySql.Mailing.ObtenerMailingPorInstId(int.Parse(instId));
                if (mailing != null && mailing.Count == 1)
                    permiso = mailing[0];
                else
                    permiso.Id = 0;

                permiso.InstId = int.Parse(instId);

                permiso.CreaCalendario = Convert.ToBoolean(creaCalendario) ? 1 : 0;
                //int.Parse(creaCalendario);
                permiso.CreaDocumento = Convert.ToBoolean(creaDocumento) ? 1 : 0;
                permiso.CreaInstitucion = Convert.ToBoolean(creaInstitucion) ? 1 : 0;
                permiso.CreaMuro = Convert.ToBoolean(creaMuro) ? 1 : 0;
                permiso.CreaProyecto = Convert.ToBoolean(creaProyecto) ? 1 : 0;
                permiso.CreaRendicion = Convert.ToBoolean(creaRendicion) ? 1 : 0;
                permiso.CreaRol = Convert.ToBoolean(creaRol) ? 1 : 0;
                permiso.CreaTricel = Convert.ToBoolean(creaTricel) ? 1 : 0;
                permiso.CreaUsuario = Convert.ToBoolean(creaUsuario) ? 1 : 0;
                permiso.EliminaCalendario = Convert.ToBoolean(eliminaCalendario) ? 1 : 0;
                permiso.EliminaDocumento = Convert.ToBoolean(eliminaDocumento) ? 1 : 0;
                permiso.EliminaInstitucion = Convert.ToBoolean(eliminaInstitucion) ? 1 : 0;
                permiso.EliminaMuro = Convert.ToBoolean(eliminaMuro) ? 1 : 0;
                permiso.EliminaProyecto = Convert.ToBoolean(eliminaProyecto) ? 1 : 0;
                permiso.EliminaRendicion = Convert.ToBoolean(eliminaRendicion) ? 1 : 0;
                permiso.EliminaRol = Convert.ToBoolean(eliminaRol) ? 1 : 0;
                permiso.EliminaTricel = Convert.ToBoolean(eliminaTricel) ? 1 : 0;
                permiso.EliminaUsuario = Convert.ToBoolean(eliminaUsuario) ? 1 : 0;
                permiso.ModificaCalendario = Convert.ToBoolean(modificaCalendario) ? 1 : 0;
                permiso.ModificaInstitucion = Convert.ToBoolean(modificaInstitucion) ? 1 : 0;
                permiso.ModificaMuro = Convert.ToBoolean(modificaMuro) ? 1 : 0;
                permiso.ModificaProyecto = Convert.ToBoolean(modificaProyecto) ? 1 : 0;
                permiso.ModificaRendicion = Convert.ToBoolean(modificaRendicion) ? 1 : 0;
                permiso.ModificaRol = Convert.ToBoolean(modificaRol) ? 1 : 0;
                permiso.ModificaTricel = Convert.ToBoolean(modificaTricel) ? 1 : 0;
                permiso.ModificaUsuario = Convert.ToBoolean(modificaUsuario) ? 1 : 0;

                string fechaStr = VCFramework.NegocioMySQL.Utiles.ConstruyeFecha(DateTime.Now);
                DateTime dt;
                DateTime.TryParseExact("01-01-2017 08:00", "yyyy-MM-dd hh:mm",
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out dt);


                
                permiso.FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                if (permiso.Id > 0)
                {
                    VCFramework.NegocioMySql.Mailing.Modificar(permiso);
                }
                else
                {
                    
                    idNuevo = VCFramework.NegocioMySql.Mailing.Insertar(permiso);
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
            string instId = data.InstId;
            //el id corresponde al rol
            if (idElemento == null)
                throw new ArgumentNullException("Id");

            VCFramework.Entidad.Mailing rol = new Mailing();

            HttpResponseMessage httpResponse = new HttpResponseMessage();


            try
            {

                rol = VCFramework.NegocioMySql.Mailing.ObtenerMailingPorId(int.Parse(idElemento));
                if (rol != null && rol.Id > 0)
                {

                        VCFramework.NegocioMySql.Mailing.Eliminar(rol);

                }


                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(rol);
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