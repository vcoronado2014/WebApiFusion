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

            #region  VotarTricel
            config.Routes.MapHttpRoute(
                name: "VotarTricel",
                routeTemplate: "api/VotarTricel",
                defaults: new
                {
                    controller = "VotarTricel"
                }
            );
            #endregion

            #region     Asistente
            config.Routes.MapHttpRoute(
                name: "Asistente",
                routeTemplate: "api/Asistente",
                defaults: new
                {
                    controller = "Asistente"
                }
            );
            #endregion

            #region     Articulo
            config.Routes.MapHttpRoute(
                name: "Articulo",
                routeTemplate: "api/Articulo",
                defaults: new
                {
                    controller = "Articulo"
                }
            );
            #endregion

            #region     Vinculo
            config.Routes.MapHttpRoute(
                name: "Vinculo",
                routeTemplate: "api/Vinculo",
                defaults: new
                {
                    controller = "Vinculo"
                }
            );
            #endregion

            #region     FileExcel
            config.Routes.MapHttpRoute(
                name: "FileExcel",
                routeTemplate: "api/FileExcel",
                defaults: new
                {
                    controller = "FileExcel"
                }
            );
            #endregion

            #region     CargaMasiva
            config.Routes.MapHttpRoute(
                name: "CargaMasiva",
                routeTemplate: "api/CargaMasiva",
                defaults: new
                {
                    controller = "CargaMasiva"
                }
            );
            #endregion

            #region     Slide
            config.Routes.MapHttpRoute(
                name: "Slide",
                routeTemplate: "api/Slide",
                defaults: new
                {
                    controller = "Slide"
                }
            );
            #endregion

            #region     ConfiguracionNodo
            config.Routes.MapHttpRoute(
                name: "ConfiguracionNodo",
                routeTemplate: "api/ConfiguracionNodo",
                defaults: new
                {
                    controller = "ConfiguracionNodo"
                }
            );
            #endregion

            #region     Inicio
            config.Routes.MapHttpRoute(
                name: "Inicio",
                routeTemplate: "api/Inicio",
                defaults: new
                {
                    controller = "Inicio"
                }
            );
            #endregion

            #region     Reporte
            config.Routes.MapHttpRoute(
                name: "Reporte",
                routeTemplate: "api/Reporte",
                defaults: new
                {
                    controller = "Reporte"
                }
            );
            #endregion

            #region  ObtenerLogin
            config.Routes.MapHttpRoute(
                name: "Login",
                routeTemplate: "api/Login",
                defaults: new
                {
                    controller = "Login"
                }
            );
            #endregion

            #region  ObtenerUsuarios
            config.Routes.MapHttpRoute(
                name: "ObtenerUsuarios",
                routeTemplate: "api/ObtenerUsuarios",
                defaults: new
                {
                    controller = "ObtenerUsuarios"
                }
            );
            #endregion

            #region  ListarUsuarios
            config.Routes.MapHttpRoute(
                name: "ListarUsuarios",
                routeTemplate: "api/ListarUsuarios",
                defaults: new
                {
                    controller = "ListarUsuarios"
                }
            );
            #endregion

            #region  ObtenerUsuario
            config.Routes.MapHttpRoute(
                name: "ObtenerUsuario",
                routeTemplate: "api/ObtenerUsuario",
                defaults: new
                {
                    controller = "ObtenerUsuario"
                }
            );
            #endregion

            #region  ObtenerRegiones
            config.Routes.MapHttpRoute(
                name: "ObtenerRegiones",
                routeTemplate: "api/ObtenerRegiones",
                defaults: new
                {
                    controller = "ObtenerRegiones"
                }
            );
            #endregion

            #region  ObtenerComunas
            config.Routes.MapHttpRoute(
                name: "ObtenerComunas",
                routeTemplate: "api/ObtenerComunas",
                defaults: new
                {
                    controller = "ObtenerComunas"
                }
            );
            #endregion

            #region  ObtenerRoles
            config.Routes.MapHttpRoute(
                name: "ObtenerRoles",
                routeTemplate: "api/ObtenerRoles",
                defaults: new
                {
                    controller = "ObtenerRoles"
                }
            );
            #endregion


            #region  Institucion
            config.Routes.MapHttpRoute(
                name: "Institucion",
                routeTemplate: "api/Institucion",
                defaults: new
                {
                    controller = "Institucion"
                }
            );
            #endregion

            #region  Rendicion
            config.Routes.MapHttpRoute(
                name: "Rendicion",
                routeTemplate: "api/Rendicion",
                defaults: new
                {
                    controller = "Rendicion"
                }
            );
            #endregion

            #region  Grafico
            config.Routes.MapHttpRoute(
                name: "Grafico",
                routeTemplate: "api/Grafico",
                defaults: new
                {
                    controller = "Grafico"
                }
            );
            #endregion

            #region  File
            config.Routes.MapHttpRoute(
                name: "File",
                routeTemplate: "api/File",
                defaults: new
                {
                    controller = "File"
                }
            );
            #endregion

            #region  FileDocumento
            config.Routes.MapHttpRoute(
                name: "FileDocumento",
                routeTemplate: "api/FileDocumento",
                defaults: new
                {
                    controller = "FileDocumento"
                }
            );
            #endregion

            #region  FileNuevo
            config.Routes.MapHttpRoute(
                name: "FileNuevo",
                routeTemplate: "api/FileNuevo",
                defaults: new
                {
                    controller = "FileNuevo"
                }
            );
            #endregion

            #region  Calendario
            config.Routes.MapHttpRoute(
                name: "Calendario",
                routeTemplate: "api/Calendario",
                defaults: new
                {
                    controller = "Calendario"
                }
            );
            #endregion

            #region  ListaTricel
            config.Routes.MapHttpRoute(
                name: "ListaTricel",
                routeTemplate: "api/ListaTricel",
                defaults: new
                {
                    controller = "ListaTricel"
                }
            );
            #endregion

            #region  PermisoRol
            config.Routes.MapHttpRoute(
                name: "PermisoRol",
                routeTemplate: "api/PermisoRol",
                defaults: new
                {
                    controller = "PermisoRol"
                }
            );
            #endregion

            #region  RolInstitucion
            config.Routes.MapHttpRoute(
                name: "RolInstitucion",
                routeTemplate: "api/RolInstitucion",
                defaults: new
                {
                    controller = "RolInstitucion"
                }
            );
            #endregion

            #region  RecuperarClave
            config.Routes.MapHttpRoute(
                name: "RecuperarClave",
                routeTemplate: "api/RecuperarClave",
                defaults: new
                {
                    controller = "RecuperarClave"
                }
            );
            #endregion

            #region  Muro
            config.Routes.MapHttpRoute(
                name: "Muro",
                routeTemplate: "api/Muro",
                defaults: new
                {
                    controller = "Muro"
                }
            );
            #endregion

            #region  RespuestaMuro
            config.Routes.MapHttpRoute(
                name: "RespuestaMuro",
                routeTemplate: "api/RespuestaMuro",
                defaults: new
                {
                    controller = "RespuestaMuro"
                }
            );
            #endregion

            #region  Mailing
            config.Routes.MapHttpRoute(
                name: "Mailing",
                routeTemplate: "api/Mailing",
                defaults: new
                {
                    controller = "Mailing"
                }
            );
            #endregion

            #region  EncabezadoCarga
            config.Routes.MapHttpRoute(
                name: "EncabezadoCarga",
                routeTemplate: "api/EncabezadoCarga",
                defaults: new
                {
                    controller = "EncabezadoCarga"
                }
            );
            #endregion

            #region  DetalleCarga
            config.Routes.MapHttpRoute(
                name: "DetalleCarga",
                routeTemplate: "api/DetalleCarga",
                defaults: new
                {
                    controller = "DetalleCarga"
                }
            );
            #endregion

            #region  Perona
            config.Routes.MapHttpRoute(
                name: "Persona",
                routeTemplate: "api/Persona",
                defaults: new
                {
                    controller = "Persona"
                }
            );
            #endregion

            #region  Solicitudes
            config.Routes.MapHttpRoute(
                name: "Solicitudes",
                routeTemplate: "api/Solicitudes",
                defaults: new
                {
                    controller = "Solicitudes"
                }
            );
            #endregion

            #region  SolMuro
            config.Routes.MapHttpRoute(
                name: "SolMuro",
                routeTemplate: "api/SolMuro",
                defaults: new
                {
                    controller = "SolMuro"
                }
            );
            #endregion

            #region  ResSolMuro
            config.Routes.MapHttpRoute(
                name: "ResSolMuro",
                routeTemplate: "api/ResSolMuro",
                defaults: new
                {
                    controller = "ResSolMuro"
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
