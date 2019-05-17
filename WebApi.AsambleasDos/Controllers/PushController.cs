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
    public class PushController : ApiController
    {

        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string dataToken = "";
            string titulo = data.Titulo;
            string texto = data.Texto;
            string instId = data.InstId;
            string usuId = data.UsuId;
            string esSolicitud = data.EsSolicitud;
            bool solicitud = true;
            bool enviar = false;
            if (data.Token != null)
            {
                dataToken = data.Token;
            }
            else
            {
                VCFramework.Entidad.TokenUsuario tokin = VCFramework.NegocioMySql.TokenUsuario.ObtenerPorToken(int.Parse(usuId), int.Parse(instId));
                if (tokin != null && tokin.Id > 0)
                {
                    dataToken = tokin.Token;
                }
                else
                {
                    enviar = false;
                }
            }

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                if (esSolicitud == "0")
                {
                    solicitud = false;
                }
                
                VCFramework.NegocioMySql.FireBasePush push = new VCFramework.NegocioMySql.FireBasePush(VCFramework.NegocioMySQL.Utiles.GetApiFirebase());
                PushMessage mensaje = new PushMessage();
                if (solicitud)
                {
                    List<string> listaStr = new List<string>();
                    //llamada para traer los tokens de la institucion
                    List<VCFramework.Entidad.TokenUsuario> tokens = VCFramework.NegocioMySql.TokenUsuario.Listar(int.Parse(instId));
                    //llamada para traer los usuarios adminsitradores
                    List<VCFramework.Entidad.UsuRol> rls = VCFramework.NegocioMySql.RlRolUsu.ObtenerPorInstId(int.Parse(instId));
                    //si existen elementos en la relacion se agregan al arreglo de string
                    if (rls != null && rls.Count > 0)
                    {
                        foreach(VCFramework.Entidad.UsuRol usu in rls)
                        {
                            if (tokens.Exists(p=>p.UsuId == usu.UsuId))
                            {
                                List<VCFramework.Entidad.TokenUsuario> tok = tokens.FindAll(p => p.UsuId == usu.UsuId).ToList();
                                if (tok != null && tok.Count > 0)
                                {
                                    foreach(VCFramework.Entidad.TokenUsuario tik in tok)
                                    {
                                        listaStr.Add(tik.Token);
                                    }
                                }
                                
                            }
                        }
                    }
                    if (listaStr != null && listaStr.Count > 0)
                    {
                        enviar = true;
                        mensaje.registration_ids = listaStr;
                    }
                }
                else
                {
                    //es un mensaje a cualquiera
                    mensaje.registration_ids = VCFramework.NegocioMySQL.Utiles.ObtenerListaTokens(int.Parse(instId));
                    enviar = true;
                }
                
                mensaje.notification = new PushMessageData();
                mensaje.notification.title = titulo;
                mensaje.notification.text = texto;
                mensaje.data = new
                {
                    Datos = "datos"
                };

                dynamic logs = null;
                if (enviar)
                {
                    logs = push.SendPush(mensaje);
                }
                

                /*
                await PushNotification.SendPushNotification();
                */
                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(logs);
                //String JSON = JsonConvert.SerializeObject("OK");
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