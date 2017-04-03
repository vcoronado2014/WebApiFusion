using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;

namespace WebApi.AsambleasDos
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            #region  Votacion
            config.Routes.MapHttpRoute(
                name: "Votacion",
                routeTemplate: "api/Votacion",
                defaults: new
                {
                    controller = "Votacion"
                }
            );
            #endregion

            #region  Tricel
            config.Routes.MapHttpRoute(
                name: "Tricel",
                routeTemplate: "api/Tricel",
                defaults: new
                {
                    controller = "Tricel"
                }
            );
            #endregion

            #region  ArchivoProyecto
            config.Routes.MapHttpRoute(
                name: "ArchivoProyecto",
                routeTemplate: "api/ArchivoProyecto",
                defaults: new
                {
                    controller = "ArchivoProyecto"
                }
            );
            #endregion

            #region  VotarProyecto
            config.Routes.MapHttpRoute(
                name: "VotarProyecto",
                routeTemplate: "api/VotarProyecto",
                defaults: new
                {
                    controller = "VotarProyecto"
                }
            );
            #endregion

            #region  ResponsableTricel
            config.Routes.MapHttpRoute(
                name: "ResponsableTricel",
                routeTemplate: "api/ResponsableTricel",
                defaults: new
                {
                    controller = "ResponsableTricel"
                }
            );
            #endregion

            #region  UsuarioLista
            config.Routes.MapHttpRoute(
                name: "UsuarioLista",
                routeTemplate: "api/UsuarioLista",
                defaults: new
                {
                    controller = "UsuarioLista"
                }
            );
            #endregion

            #region  Contacto
            config.Routes.MapHttpRoute(
                name: "Contacto",
                routeTemplate: "api/Contacto",
                defaults: new
                {
                    controller = "Contacto"
                }
            );
            #endregion

            #region  Logs
            config.Routes.MapHttpRoute(
                name: "Logs",
                routeTemplate: "api/Logs",
                defaults: new
                {
                    controller = "Logs"
                }
            );
            #endregion

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
