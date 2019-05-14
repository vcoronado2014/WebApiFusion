﻿using System;
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

            string dataToken = data.Token;


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                
                FireBasePush push = new FireBasePush(VCFramework.NegocioMySQL.Utiles.GetApiFirebase());
                PushMessage mensaje = new PushMessage();
                List<string> listaStr = new List<string>();
                //string arr = VCFramework.NegocioMySql.TokenUsuario.Listar().ToString();
                string arr = "";
                List<VCFramework.Entidad.TokenUsuario> tokens = VCFramework.NegocioMySql.TokenUsuario.Listar();
                if (tokens != null && tokens.Count > 0)
                {
                    foreach(VCFramework.Entidad.TokenUsuario tok in tokens)
                    {
                        listaStr.Add(tok.Token);
                    }
                }
                arr = dataToken;
                mensaje.to = arr;
                mensaje.notification = new PushMessageData();
                mensaje.notification.title = "Mensaje desde la API";
                mensaje.notification.text = "Este mensaje fue enviado desde la API, ES para pruebas de envío";
                mensaje.data = new
                {
                    Datos = "datos"
                };
                dynamic logs = push.SendPush(mensaje);
                

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