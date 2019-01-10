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
using System.Net.Mail;

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SolMuroController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [AgendaExceptionFilter]
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]string id)
        {

            //validaciones antes de ejecutar la llamada.
            if (id == "")
                throw new ArgumentNullException("Id");

            VCFramework.EntidadFuncional.SolMuroFuncional entidad = new VCFramework.EntidadFuncional.SolMuroFuncional();


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                //string instId = data.InstId;
                int idBuscar = int.Parse(id);

                VCFramework.Entidad.SolMuro muro = VCFramework.NegocioMySql.SolMuro.ObtenerMuroPorId(idBuscar);


                if (muro != null && muro.Id > 0)
                {
                    entidad.Eliminado = muro.Eliminado;
                    entidad.FechaCreacion = muro.FechaCreacion;
                    entidad.Id = muro.Id;
                    entidad.InstId = muro.InstId;
                    entidad.PrioridadId = muro.PrioridadId;
                    entidad.RolId = muro.RolId;
                    entidad.Texto = muro.Texto;
                    entidad.UrlImagen = muro.UrlImagen;
                    entidad.UsuId = muro.UsuId;
                    //entidad.NombreUsuario = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(muro.UsuId).NombreUsuario;
                    int idUsuario = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(muro.UsuId).Id;
                    VCFramework.Entidad.Persona persona = VCFramework.NegocioMySQL.Persona.ObtenerPersonaPorUsuId(idUsuario);
                    entidad.NombreUsuario = persona.Nombres + ' ' + persona.ApellidoPaterno;
                    //*******************************************************************************************************************
                    entidad.NombreRol = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolPorId(muro.RolId).Nombre;
                    entidad.VisibleEliminar = true;
                    string fechita = VCFramework.NegocioMySQL.Utiles.DiferenciaFechas(DateTime.Now, entidad.FechaCreacion);
                    entidad.FechaString = fechita;
                    List<VCFramework.Entidad.ResSolmuro> respuestaMuro = VCFramework.NegocioMySql.ResSolmuro.ObtenerRespuestaMuroPorMuroId(entidad.Id);
                    entidad.RespuestaMuro = new List<VCFramework.EntidadFuncional.ResSolmuroFuncional>();
                    if (respuestaMuro != null && respuestaMuro.Count > 0)
                    {
                        foreach (VCFramework.Entidad.ResSolmuro resp in respuestaMuro)
                        {
                            VCFramework.EntidadFuncional.ResSolmuroFuncional respF = new VCFramework.EntidadFuncional.ResSolmuroFuncional();
                            respF.Eliminado = resp.Eliminado;
                            respF.MroId = resp.MroId;
                            respF.FechaCreacion = resp.FechaCreacion;
                            respF.Id = resp.Id;
                            respF.InstId = resp.InstId;
                            respF.PrioridadId = resp.PrioridadId;
                            respF.RolId = resp.RolId;
                            respF.Texto = resp.Texto;
                            respF.UrlImagen = resp.UrlImagen;
                            respF.UsuId = resp.UsuId;
                            //respF.NombreUsuario = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(resp.UsuId).NombreUsuario;
                            int idUsuarioF = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(resp.UsuId).Id;
                            VCFramework.Entidad.Persona personaF = VCFramework.NegocioMySQL.Persona.ObtenerPersonaPorUsuId(idUsuarioF);
                            respF.NombreUsuario = personaF.Nombres + ' ' + personaF.ApellidoPaterno;
                            //*******************************************************************************************************************
                            respF.NombreRol = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolPorId(resp.RolId).Nombre;
                            string fechita2 = VCFramework.NegocioMySQL.Utiles.DiferenciaFechas(DateTime.Now, resp.FechaCreacion);
                            respF.FechaString = fechita2;
                            entidad.VisibleEliminar = false;
                            entidad.RespuestaMuro.Add(respF);
                        }
                        if (entidad.RespuestaMuro != null && entidad.RespuestaMuro.Count > 0)
                            entidad.RespuestaMuro = entidad.RespuestaMuro.OrderByDescending(p => p.FechaCreacion).ToList();
                    }


                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(entidad);
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

            string idInstitucion = data.InstId;
            string usuIdLogueado = data.UsuId;

            List<VCFramework.EntidadFuncional.SolMuroFuncional> lista = new List<VCFramework.EntidadFuncional.SolMuroFuncional>();

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            //tendriamos que verificar si el usuario que esta consultando se encuentra dentro de los usuarios de la lista
            //que se encuentran autorizados, para eso debería traerme los elementos de la institucionId y usuId
            //si el usuario se encuentra dentro de la lista entonces debo listar todas las solicitudes del muro
            //si el usuario no se encuentra dentro de la lista tendrìa que traerme todos los mensajes generados por dicho
            //usuario (el control de acceso esta en el front)


            try
            {
                int idBuscar = int.Parse(idInstitucion);
                //validamos antes
                List<VCFramework.Entidad.UsuRol> ususRol = VCFramework.NegocioMySql.RlRolUsu.ObtenerPorInstId(idBuscar);
                //es la persona
                VCFramework.Entidad.Persona persona = VCFramework.NegocioMySQL.Persona.ObtenerPersonaPorUsuId(int.Parse(usuIdLogueado));

                bool esAdministrador = false;
                if (persona != null && persona.Id > 0)
                  esAdministrador = ususRol.Exists(p=>p.UsuId == persona.UsuId);

                //aca procesamos todo!
                List<VCFramework.Entidad.SolMuro> muro = new List<SolMuro>();

                if (esAdministrador)
                    muro = VCFramework.NegocioMySql.SolMuro.ObtenerMuroPorInstId(idBuscar);
                else
                    muro = VCFramework.NegocioMySql.SolMuro.ObtenerMuroPorInstId(idBuscar).FindAll(p => p.UsuId == int.Parse(usuIdLogueado));


                if (muro != null && muro.Count > 0)
                {
                    muro = muro.FindAll(p => p.Eliminado == 0);
                    foreach (VCFramework.Entidad.SolMuro entidad in muro)
                    {
                        VCFramework.EntidadFuncional.SolMuroFuncional muroF = new VCFramework.EntidadFuncional.SolMuroFuncional();
                        muroF.Eliminado = entidad.Eliminado;
                        muroF.FechaCreacion = entidad.FechaCreacion;
                        muroF.Id = entidad.Id;
                        muroF.InstId = entidad.InstId;
                        muroF.PrioridadId = entidad.PrioridadId;
                        muroF.RolId = entidad.RolId;
                        muroF.Texto = entidad.Texto;
                        muroF.UrlImagen = entidad.UrlImagen;
                        muroF.UsuId = entidad.UsuId;
                        //muroF.NombreUsuario = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(entidad.UsuId).NombreUsuario;
                        int idUsuario = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(muroF.UsuId).Id;
                        VCFramework.Entidad.Persona personaUsu = VCFramework.NegocioMySQL.Persona.ObtenerPersonaPorUsuId(idUsuario);
                        muroF.NombreUsuario = personaUsu.Nombres + ' ' + personaUsu.ApellidoPaterno;
                        //*******************************************************************************************************************

                        //muroF.NombreUsuario = persona.Nombres + ' ' + persona.ApellidoPaterno;
                        //*******************************************************************************************************************
                        muroF.NombreRol = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolPorId(entidad.RolId).Nombre;
                        muroF.VisibleEliminar = true;
                        if (int.Parse(usuIdLogueado) != muroF.UsuId)
                            muroF.VisibleEliminar = false;
                        string fechita = VCFramework.NegocioMySQL.Utiles.DiferenciaFechas(DateTime.Now, entidad.FechaCreacion);
                        muroF.FechaString = fechita;

                        List<VCFramework.Entidad.ResSolmuro> respuestaMuro = VCFramework.NegocioMySql.ResSolmuro.ObtenerRespuestaMuroPorMuroId(entidad.Id);
                        muroF.RespuestaMuro = new List<VCFramework.EntidadFuncional.ResSolmuroFuncional>();
                        if (respuestaMuro != null && respuestaMuro.Count > 0)
                        {
                            respuestaMuro = respuestaMuro.FindAll(p => p.Eliminado == 0);
                            foreach (VCFramework.Entidad.ResSolmuro resp in respuestaMuro)
                            {
                                VCFramework.EntidadFuncional.ResSolmuroFuncional respF = new VCFramework.EntidadFuncional.ResSolmuroFuncional();
                                respF.Eliminado = resp.Eliminado;
                                respF.MroId = resp.MroId;
                                respF.FechaCreacion = resp.FechaCreacion;
                                respF.Id = resp.Id;
                                respF.InstId = resp.InstId;
                                respF.PrioridadId = resp.PrioridadId;
                                respF.RolId = resp.RolId;
                                respF.Texto = resp.Texto;
                                respF.UrlImagen = resp.UrlImagen;
                                respF.UsuId = resp.UsuId;
                                //respF.NombreUsuario = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(resp.UsuId).NombreUsuario;
                                int idUsuarioUsu = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(respF.UsuId).Id;
                                VCFramework.Entidad.Persona personaF = VCFramework.NegocioMySQL.Persona.ObtenerPersonaPorUsuId(idUsuarioUsu);
                                respF.NombreUsuario = personaF.Nombres + ' ' + personaF.ApellidoPaterno;
                                //*******************************************************************************************************************
                                respF.NombreRol = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolPorId(resp.RolId).Nombre;
                                string fechita2 = VCFramework.NegocioMySQL.Utiles.DiferenciaFechas(DateTime.Now, resp.FechaCreacion);
                                respF.FechaString = fechita2;
                                muroF.VisibleEliminar = false;
                                respF.VisibleEliminar = true;
                                if (int.Parse(usuIdLogueado) != respF.UsuId)
                                    respF.VisibleEliminar = false;
                                muroF.RespuestaMuro.Add(respF);
                            }
                            if (muroF.RespuestaMuro != null && muroF.RespuestaMuro.Count > 0)
                                muroF.RespuestaMuro = muroF.RespuestaMuro.OrderByDescending(p => p.FechaCreacion).ToList();
                        }


                        lista.Add(muroF);

                    }
                    if (lista != null && lista.Count > 0)
                        lista = lista.OrderByDescending(p => p.FechaCreacion).ToList();

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

        [System.Web.Http.AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (data.Id == 0)
                throw new ArgumentNullException("Id");

            string esCpasStr = "false";
            bool esCpas = false;
            if (data.EsCpas != null)
            {
                esCpasStr = data.EsCpas;
                esCpas = Convert.ToBoolean(esCpasStr);
            }


            HttpResponseMessage httpResponse = new HttpResponseMessage();



            try
            {
                string idInstitucion = data.Id;
                int idInstitucionBuscar = int.Parse(idInstitucion);
                VCFramework.Entidad.SolMuro inst = VCFramework.NegocioMySql.SolMuro.ObtenerMuroPorId(idInstitucionBuscar);

                if (inst != null && inst.Id > 0)
                {
                    inst.Eliminado = 1;

                    VCFramework.NegocioMySql.SolMuro.Modificar(inst);
                    List<VCFramework.Entidad.ResSolmuro> respuestas = VCFramework.NegocioMySql.ResSolmuro.ObtenerRespuestaMuroPorMuroId(inst.Id);
                    if (respuestas != null && respuestas.Count > 0)
                    {
                        foreach (VCFramework.Entidad.ResSolmuro resp in respuestas)
                        {
                            resp.Eliminado = 1;
                            VCFramework.NegocioMySql.ResSolmuro.Eliminar(resp);

                            List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(inst.InstId);
                            List<string> listaCorreos = new List<string>();
                            if (correos != null && correos.Count > 0)
                            {
                                foreach (UsuariosCorreos us in correos)
                                {
                                    if (!listaCorreos.Exists(p => p == us.Correo))
                                        listaCorreos.Add(us.Correo);
                                }
                            }
                            if (listaCorreos != null && listaCorreos.Count > 0)
                            {
                                VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(inst.InstId);
                                VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                                //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, false);
                                MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeMuro(institucion.Id, institucion.Nombre, inst.Texto, listaCorreos, false, false, true, esCpas);

                                //cr.Enviar(mnsj);
                                var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                            }

                        }
                    }

                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(inst);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);

                }


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

            string idInstitucion = data.Id;
            if (idInstitucion == null)
                idInstitucion = "0";
            int idInstitucionBuscar = int.Parse(idInstitucion);


            //validaciones antes de ejecutar la llamada.
            VCFramework.Entidad.SolMuro aus = VCFramework.NegocioMySql.SolMuro.ObtenerMuroPorId(idInstitucionBuscar);

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string instId = data.InstId;
                string usuId = data.UsuId;
                string prioridadId = data.PrioridadId;
                string rolId = data.RolId;
                string texto = data.Texto;

                string esCpasStr = "false";
                bool esCpas = false;
                if (data.EsCpas != null)
                {
                    esCpasStr = data.EsCpas;
                    esCpas = Convert.ToBoolean(esCpasStr);
                }

                if (aus == null)
                    aus = new VCFramework.Entidad.SolMuro();

                if (aus != null)
                {
                    int nuevoId = 0;

                    //prioridadId = 0 baja, 1 media, 2 alta
                    if (int.Parse(prioridadId) == 0)
                        aus.UrlImagen = "img/green.png";
                    if (int.Parse(prioridadId) == 1)
                        aus.UrlImagen = "img/blue.png";
                    if (int.Parse(prioridadId) == 2)
                        aus.UrlImagen = "img/red.png";

                    aus.FechaCreacion = DateTime.Now;
                    aus.InstId = int.Parse(instId);
                    aus.PrioridadId = int.Parse(prioridadId);
                    aus.RolId = int.Parse(rolId);
                    aus.Texto = texto;
                    aus.UsuId = int.Parse(usuId);

                    if (aus.Id == 0)
                    {
                        nuevoId = VCFramework.NegocioMySql.SolMuro.Insertar(aus);
                        List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(aus.InstId);
                        List<string> listaCorreos = new List<string>();
                        if (correos != null && correos.Count > 0)
                        {
                            foreach (UsuariosCorreos us in correos)
                            {
                                if (!listaCorreos.Exists(p => p == us.Correo))
                                    listaCorreos.Add(us.Correo);
                            }
                        }
                        if (listaCorreos != null && listaCorreos.Count > 0)
                        {
                            VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(aus.InstId);
                            VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                            //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, false);
                            MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeMuro(institucion.Id, institucion.Nombre, aus.Texto, listaCorreos, true, false, false, esCpas);

                            //cr.Enviar(mnsj);
                            var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                        }
                    }
                    else
                    {
                        nuevoId = aus.Id;
                        VCFramework.NegocioMySql.SolMuro.Modificar(aus);

                        List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(aus.InstId);
                        List<string> listaCorreos = new List<string>();
                        if (correos != null && correos.Count > 0)
                        {
                            foreach (UsuariosCorreos us in correos)
                            {
                                if (!listaCorreos.Exists(p => p == us.Correo))
                                    listaCorreos.Add(us.Correo);
                            }
                        }
                        if (listaCorreos != null && listaCorreos.Count > 0)
                        {
                            VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(aus.InstId);
                            VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                            //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, false);
                            MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeMuro(institucion.Id, institucion.Nombre, aus.Texto, listaCorreos, false, true, false, esCpas);

                            //cr.Enviar(mnsj);
                            var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                        }
                    }


                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(aus);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);


                }
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