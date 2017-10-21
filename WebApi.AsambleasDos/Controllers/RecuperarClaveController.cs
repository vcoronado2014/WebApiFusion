using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Xml;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using VCFramework.Entidad;
using VCFramework.NegocioMySQL;
using System.Net.Mail;

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RecuperarClaveController : ApiController
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
            if (data.NombreUsuario == "")
                throw new ArgumentNullException("NombreUsuario");

            string esCpasStr = "false";
            bool esCpas = false;
            if (data.EsCpas != null)
            {
                esCpasStr = data.EsCpas;
                esCpas = Convert.ToBoolean(esCpasStr);
            }

            VCFramework.Entidad.AutentificacionUsuario entidadAus = new VCFramework.Entidad.AutentificacionUsuario();
            VCFramework.Entidad.Persona entidadPersona = new VCFramework.Entidad.Persona();

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string usu = data.NombreUsuario;
                bool existeAus = false;
                bool existePersona = false;
                //lo primero es verificar por el nombreUsuario
                List<VCFramework.Entidad.AutentificacionUsuario> aus = VCFramework.NegocioMySQL.AutentificacionUsuario.ListarUsuarios();

                if (aus != null && aus.Count > 0)
                {
                    if (aus.Exists(p=>p.NombreUsuario.ToUpper() == usu.ToUpper() && p.Eliminado == 0 && p.EsVigente == 1))
                    {
                        entidadAus = aus.FirstOrDefault(p => p.NombreUsuario.ToUpper() == usu.ToUpper() && p.Eliminado == 0 && p.EsVigente == 1);
                        if (entidadAus != null && entidadAus.Id > 0)
                        {
                            string nuevaPass = VCFramework.NegocioMySQL.Utiles.DesEncriptar(entidadAus.Password);
                            entidadAus.Password = nuevaPass;
                            existeAus = true;
                        }
                    }
                }

                if (existeAus == false)
                {
                    List<VCFramework.Entidad.Persona> personas = VCFramework.NegocioMySQL.Persona.ListarPersonas();
                    if (personas != null && personas.Count > 0)
                    {
                        //hay que encriptar
                        var rut = usu;
                        if (personas.Exists(p => p.Rut.ToUpper() == rut.ToUpper() && p.Eliminado == 0))
                        {
                            entidadPersona = personas.FirstOrDefault(p => p.Rut.ToUpper() == rut.ToUpper() && p.Eliminado == 0);
                            if (entidadPersona != null && entidadPersona.UsuId > 0)
                            {
                                entidadAus = aus.FirstOrDefault(p => p.Id == entidadPersona.UsuId && p.Eliminado == 0 && p.EsVigente == 1);
                                if (entidadAus != null && entidadAus.Id > 0)
                                {
                                    string nuevaPass = VCFramework.NegocioMySQL.Utiles.DesEncriptar(entidadAus.Password);
                                    entidadAus.Password = nuevaPass;
                                    existePersona = true;
                                }

                            }

                        }
                    }
                }

                if (entidadAus != null && entidadAus.Id > 0 && entidadAus.CorreoElectronico != string.Empty)
                {
                    //ahora mandar el correo
                    
                    VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                    MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeRecuperarClave(entidadAus.NombreUsuario, entidadAus.Password, entidadAus.CorreoElectronico, esCpas);
                    //cr.Enviar(mnsj);
                    var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(entidadAus);
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