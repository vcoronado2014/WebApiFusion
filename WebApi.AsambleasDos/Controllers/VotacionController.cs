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
using System.IO;
using System.Text;
using System.Web;
using System.Net.Mail;

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VotacionController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage Get([FromUri]string InstId)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

            //validaciones antes de ejecutar la llamada.
            if (InstId == "0")
                throw new ArgumentNullException("id");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {

                int instIdBuscar = int.Parse(InstId);

                VCFramework.EntidadFuncional.proposalss votaciones = new VCFramework.EntidadFuncional.proposalss();
                votaciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.Tricel> triceles = VCFramework.NegocioMySQL.Tricel.ObtenerTricelPorInstIdTodos(instIdBuscar);


                if (triceles != null && triceles.Count > 0)
                {
                    foreach (VCFramework.Entidad.Tricel tri in triceles)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = tri.Id;
                        us.NombreCompleto = tri.Objetivo;
                        us.NombreUsuario = tri.Nombre;

                        us.OtroUno = tri.FechaInicio;
                        us.OtroDos = tri.FechaTermino;

                        //us.OtroDos = DateTime.Parse(tri.FechaTermino, culture).ToShortDateString();
                        us.OtroTres = tri.FechaCreacion.ToShortDateString();
                        //cantidad de listas asociadas al tricel
                        List<VCFramework.Entidad.ListaTricel> listas = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaTricelPorTricelId(tri.Id);
                        us.OtroCuatro = listas.Count.ToString();
                        us.OtroCinco = us.OtroUno + " - " + us.OtroDos;
                        //us.UrlDocumento = insti.UrlDocumento;
                        List<VCFramework.Entidad.ResponsableTricel> responsables = VCFramework.NegocioMySQL.ResponsableTricel.ObtenerResponsables(tri.Id);
                        if (responsables != null && responsables.Count > 0)
                            us.OtroOcho = responsables[0].UsuId.ToString();
                        else
                            us.OtroOcho = "0";

                        us.OtroDiez = tri.QuorumMinimo.ToString();


                        us.Url = "CrearModificarVotacion.html?id=" + us.Id.ToString() + "&ELIMINAR=0";
                        us.UrlEliminar = "CrearModificarVotacion.html?id=" + us.Id.ToString() + "&ELIMINAR=1";


                        votaciones.proposals.Add(us);
                    }
                    //establecimientos.Establecimientos = instituciones;
                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(votaciones);
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
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string buscarNombreUsuario = "";
            if (data.BuscarId != null)
            {
                buscarNombreUsuario = data.BuscarId;
            }

            //validaciones antes de ejecutar la llamada.
            if (data.InstId == 0)
                throw new ArgumentNullException("InstId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string instId = data.InstId;
                int instIdBuscar = int.Parse(instId);
                string totalUsuarios = VCFramework.NegocioMySQL.AutentificacionUsuario.ListarUsuariosPorInstId(int.Parse(instId)).Count.ToString();

                VCFramework.EntidadFuncional.proposalss votaciones = new VCFramework.EntidadFuncional.proposalss();
                votaciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.Tricel> triceles = VCFramework.NegocioMySQL.Tricel.ObtenerTricelPorInstIdTodos(instIdBuscar);

                if (buscarNombreUsuario != "")
                {
                    if (VCFramework.NegocioMySQL.Utiles.IsNumeric(buscarNombreUsuario))
                        triceles = triceles.FindAll(p => p.Id == int.Parse(buscarNombreUsuario));
                    else
                        triceles = triceles.FindAll(p => p.Nombre.ToUpper().Replace(" ", "") == buscarNombreUsuario.ToUpper().Replace(" ", ""));
                }
                StringBuilder sbTextoVoto = new StringBuilder();

                if (triceles != null && triceles.Count > 0)
                {
                    foreach (VCFramework.Entidad.Tricel tri in triceles)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = tri.Id;
                        us.NombreCompleto = tri.Objetivo;
                        us.NombreUsuario = tri.Nombre;

                        us.OtroUno = tri.FechaInicio;
                        us.OtroDos = tri.FechaTermino;
                        //us.OtroTres = tri.FechaCreacion.ToShortDateString();
                        
                        us.OtroTres = tri.FechaCreacion.ToShortDateString().Replace('/', '-');
                        //cantidad de listas asociadas al tricel
                        List<VCFramework.Entidad.ListaTricel> listas = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaTricelPorTricelId(tri.Id);
                        us.OtroCuatro = listas.Count.ToString();

                        if (listas.Count > 0)
                        {
                            if (data.UsuId != null)
                            {
                                string usuId = data.UsuId;
                                List<VCFramework.Entidad.VotTricel> votosTricel = VCFramework.NegocioMySQL.VotTricel.ObtenerVotacionPorUsuario(int.Parse(usuId), tri.Id);
                                if (votosTricel != null && votosTricel.Count > 0)
                                {
                                    us.OtroSiete = "0";
                                    sbTextoVoto.AppendFormat("Usted ya realizó su voto el día {0}.", votosTricel[0].FechaVotacion.ToShortDateString());
                                }
                                else
                                {
                                    sbTextoVoto.Append("Usted aún no ha votado");
                                    us.OtroSiete = "1";
                                }
                            }
                            else
                            {
                                sbTextoVoto.Append("Usted aún no ha votado");
                                us.OtroSiete = "1";
                            }
                        }
                        else
                        {
                            sbTextoVoto.Append("Usted aún no ha votado");
                            us.OtroSiete = "1";
                        }
                        //us.UrlDocumento = insti.UrlDocumento;
                        us.OtroCinco = us.OtroUno + " - " + us.OtroDos;
                        List<VCFramework.Entidad.ResponsableTricel> responsables = VCFramework.NegocioMySQL.ResponsableTricel.ObtenerResponsables(tri.Id);
                        if (responsables != null && responsables.Count > 0)
                            us.OtroOcho = responsables[0].UsuId.ToString();
                        else
                            us.OtroOcho = "0";

                        us.OtroNueve = sbTextoVoto.ToString();
                        us.OtroDiez = tri.QuorumMinimo.ToString();
                        //votaciones de las listas
                        int cantidadVotaciones = VCFramework.NegocioMySQL.VotTricel.ObtenerVotacionesPorTricelId(tri.Id).Count;
                        //aca la cantidad de votaciones
                        us.OtroOnce = cantidadVotaciones.ToString();
                        //aca el quorum
                        us.OtroDoce = tri.QuorumMinimo.ToString();
                        //total usuarios
                        us.TotalUsuarios = totalUsuarios;

                        us.Url = "CrearModificarVotacion.html?id=" + us.Id.ToString() + "&ELIMINAR=0";
                        us.UrlEliminar = "CrearModificarVotacion.html?id=" + us.Id.ToString() + "&ELIMINAR=1";


                        votaciones.proposals.Add(us);
                    }
                    //establecimientos.Establecimientos = instituciones;
                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(votaciones);
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
            if (id == null)
                id = "0";

            string instId = data.InstId;
            string nombre = data.Nombre;
            string objetivo = data.Objetivo;
            string fechaInicio = data.FechaInicio;
            string fechaTermino = data.FechaTermino;
            string usuId = data.IdUsuario;
            string usuIdResponsable = data.UsuIdResponsable;
            string quorumMinimo = "0";

            if (data.QuorumMinimo != null)
            {
                quorumMinimo = data.QuorumMinimo;
            }


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            int idNuevo = 0;



            try
            {
                VCFramework.Entidad.Tricel tricel = new VCFramework.Entidad.Tricel();
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");
                IFormatProvider culture1 = new System.Globalization.CultureInfo("es-CL", true);
                if (id != "0")
                {
                    //es modificado
                    List<VCFramework.Entidad.Tricel> triceles = VCFramework.NegocioMySQL.Tricel.ObtenerTricelPorId(int.Parse(id));
                    if (triceles.Count == 1)
                    {
                        tricel = triceles[0];
                        tricel.FechaInicio = fechaInicio;
                        tricel.FechaTermino = fechaTermino;
                        tricel.Nombre = nombre;
                        tricel.Objetivo = objetivo;
                        tricel.QuorumMinimo = int.Parse(quorumMinimo);
                        VCFramework.NegocioMySQL.Tricel.Modificar(tricel);
                        //ahora actualizamos las listas
                        List<VCFramework.Entidad.ListaTricel> listas = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaTricelPorTricelId(tricel.Id);
                        if (listas != null && listas.Count > 0)
                        {
                            foreach (VCFramework.Entidad.ListaTricel list in listas)
                            {
                                list.FechaInicio = tricel.FechaInicio;
                                list.FechaTermino = tricel.FechaTermino;
                                VCFramework.NegocioMySQL.ListaTricel.Modificar(list);

                            }
                        }
                        List<VCFramework.Entidad.ResponsableTricel> responsable = VCFramework.NegocioMySQL.ResponsableTricel.ObtenerResponsables(tricel.Id);
                        if (responsable != null && responsable.Count == 1)
                        {
                            VCFramework.Entidad.ResponsableTricel resp = responsable[0];
                            resp.UsuId = int.Parse(usuIdResponsable);
                            //modificar
                            VCFramework.NegocioMySQL.ResponsableTricel.Modificar(resp);

                        }
                        else
                        {
                            VCFramework.Entidad.ResponsableTricel resp = new VCFramework.Entidad.ResponsableTricel();
                            resp.Eliminado = 0;
                            resp.TriId = tricel.Id;
                            resp.UsuId = int.Parse(usuIdResponsable);
                            VCFramework.NegocioMySQL.ResponsableTricel.Insertar(resp);
                        }

                        if (tricel.Id >= 0)
                        {
                            #region manejo del evento
                            //ojo acá, hay que modificar el evento también
                            //para este caso vamos a utilizar el tipo 2 que será tricel
                            //el Status contendrá el id del elemento original
                            //falta agregar fechas
                            //24-03-2017
                            string[] fechaIniArr = tricel.FechaInicio.Split('-');
                            DateTime fechaIni = new DateTime(
                                int.Parse(fechaIniArr[2]),
                                int.Parse(fechaIniArr[1]),
                                int.Parse(fechaIniArr[0]),
                                0, 0, 0
                                );
                            string[] fechaTerArr = tricel.FechaTermino.Split('-');
                            DateTime fechaTer = new DateTime(
                                int.Parse(fechaTerArr[2]),
                                int.Parse(fechaTerArr[1]),
                                int.Parse(fechaTerArr[0]),
                                23, 59, 0
                                );

                            VCFramework.Entidad.Calendario calendario = new VCFramework.Entidad.Calendario();
                            List<VCFramework.Entidad.Calendario> listaCalendario = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstidTipo(tricel.InstId, 3);
                            if (listaCalendario != null && listaCalendario.Count > 0)
                            {

                                listaCalendario = listaCalendario.FindAll(p => p.Status == tricel.Id);
                                if (listaCalendario != null && listaCalendario.Count > 0)
                                {
                                    calendario = listaCalendario[0];
                                }
                            }
                            //seteamos los valores
                            calendario.Titulo = tricel.Nombre;
                            calendario.Detalle = tricel.Nombre;
                            calendario.Asunto = tricel.Nombre;
                            calendario.Url = "";
                            calendario.Ubicacion = "CPAS";
                            calendario.InstId = tricel.InstId;
                            calendario.Etiqueta = 1;
                            calendario.Descripcion = tricel.Nombre;
                            calendario.Status = tricel.Id;
                            calendario.Tipo = 3;
                            calendario.FechaInicio = fechaIni;
                            calendario.FechaTermino = fechaTer;
                            calendario.UsuIdCreador = tricel.UsuIdCreador;
                            if (calendario.Id > 0)
                            {
                                //MODIFICAR
                                VCFramework.NegocioMySQL.Calendario.Modificar(calendario);
                            }
                            else
                            {
                                VCFramework.NegocioMySQL.Calendario.Insertar(calendario);
                            }

                            #endregion



                            List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(int.Parse(instId));
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
                                VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(int.Parse(instId));
                                VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                                //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, false);
                                MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeTricel(institucion.Id, institucion.Nombre, tricel.Nombre, listaCorreos, false, true, false);

                                //cr.Enviar(mnsj);
                                var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                            }


                        }
                    }
                }
                else
                {
                    //es nuevo
                    tricel.Eliminado = 0;
                    tricel.EsVigente = 1;
                    tricel.FechaInicio = fechaInicio;
                    tricel.FechaTermino = fechaTermino;
                    tricel.Nombre = nombre;
                    tricel.Objetivo = objetivo;
                    tricel.InstId = int.Parse(instId);
                    tricel.UsuIdCreador = int.Parse(usuId);
                    tricel.FechaCreacion = DateTime.Now;
                    tricel.QuorumMinimo = int.Parse(quorumMinimo);
                    tricel.Id = VCFramework.NegocioMySQL.Tricel.Insertar(tricel);

                    VCFramework.Entidad.ResponsableTricel resp = new VCFramework.Entidad.ResponsableTricel();
                    resp.Eliminado = 0;
                    resp.TriId = tricel.Id;
                    resp.UsuId = int.Parse(usuIdResponsable);
                    int respuesta = VCFramework.NegocioMySQL.ResponsableTricel.Insertar(resp);
                    //envio del correo
                    if (respuesta >=0)
                    {
                        #region manejo del evento
                        //ojo acá, hay que modificar el evento también
                        //para este caso vamos a utilizar el tipo 3 que será tricel
                        //el Status contendrá el id del elemento original
                        //falta agregar fechas
                        //24-03-2017
                        string[] fechaIniArr = tricel.FechaInicio.Split('-');
                        DateTime fechaIni = new DateTime(
                            int.Parse(fechaIniArr[2]),
                            int.Parse(fechaIniArr[1]),
                            int.Parse(fechaIniArr[0]),
                            0, 0, 0
                            );
                        string[] fechaTerArr = tricel.FechaTermino.Split('-');
                        DateTime fechaTer = new DateTime(
                            int.Parse(fechaTerArr[2]),
                            int.Parse(fechaTerArr[1]),
                            int.Parse(fechaTerArr[0]),
                            23, 59, 0
                            );

                        VCFramework.Entidad.Calendario calendario = new VCFramework.Entidad.Calendario();
                        List<VCFramework.Entidad.Calendario> listaCalendario = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstidTipo(tricel.InstId, 3);
                        if (listaCalendario != null && listaCalendario.Count > 0)
                        {

                            listaCalendario = listaCalendario.FindAll(p => p.Status == tricel.Id);
                            if (listaCalendario != null && listaCalendario.Count > 0)
                            {
                                calendario = listaCalendario[0];
                            }
                        }
                        //seteamos los valores
                        calendario.Titulo = tricel.Nombre;
                        calendario.Detalle = tricel.Nombre;
                        calendario.Asunto = tricel.Nombre;
                        calendario.Url = "";
                        calendario.Ubicacion = "CPAS";
                        calendario.InstId = tricel.InstId;
                        calendario.Etiqueta = 1;
                        calendario.Descripcion = tricel.Nombre;
                        calendario.Status = respuesta;
                        calendario.Tipo = 3;
                        calendario.FechaInicio = fechaIni;
                        calendario.FechaTermino = fechaTer;
                        calendario.UsuIdCreador = tricel.UsuIdCreador;
                        if (calendario.Id > 0)
                        {
                            //MODIFICAR
                            VCFramework.NegocioMySQL.Calendario.Modificar(calendario);
                        }
                        else
                        {
                            VCFramework.NegocioMySQL.Calendario.Insertar(calendario);
                        }

                        #endregion

                        List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(int.Parse(instId));
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
                            VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(int.Parse(instId));
                            VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                            //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, false);
                            MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeTricel(institucion.Id, institucion.Nombre, tricel.Nombre, listaCorreos, true, false, false);

                            //cr.Enviar(mnsj);
                            var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                        }

                    }
                }


                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(tricel);
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

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string id = data.Id;
                int idBuscar = int.Parse(id);


                VCFramework.Entidad.Tricel inst = VCFramework.NegocioMySQL.Tricel.ObtenerTricelPorId(idBuscar)[0];

                if (inst != null && inst.Id > 0)
                {
                    inst.Eliminado = 1;


                    VCFramework.NegocioMySQL.Tricel.Modificar(inst);
                    //archivos tricel
                    List<VCFramework.Entidad.ArchivosTricel> archivos = VCFramework.NegocioMySQL.ArchivosTricel.ObtenerArchivosPorTricelId(idBuscar, null);
                    if (archivos != null && archivos.Count > 0)
                    {
                        foreach (VCFramework.Entidad.ArchivosTricel arc in archivos)
                        {
                            VCFramework.NegocioMySQL.ArchivosTricel.Eliminar(arc);
                        }
                    }
                    //listas tricel
                    List<VCFramework.Entidad.ListaTricel> listas = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaTricelPorTricelId(idBuscar);
                    if (listas != null && listas.Count > 0)
                    {
                        foreach (VCFramework.Entidad.ListaTricel list in listas)
                        {
                            VCFramework.NegocioMySQL.ListaTricel.Eliminar(list);
                        }
                    }

                    #region calendario
                    VCFramework.Entidad.Calendario calendario = new VCFramework.Entidad.Calendario();
                    List<VCFramework.Entidad.Calendario> listaCalendario = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstidTipo(inst.InstId, 2);
                    if (listaCalendario != null && listaCalendario.Count > 0)
                    {

                        listaCalendario = listaCalendario.FindAll(p => p.Status == idBuscar);
                        if (listaCalendario != null && listaCalendario.Count > 0)
                        {
                            calendario = listaCalendario[0];
                        }
                    }
                    if (calendario.Id > 0)
                    {
                        calendario.Eliminado = 1;
                        VCFramework.NegocioMySQL.Calendario.Modificar(calendario);
                    }
                    #endregion

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
                        MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeTricel(institucion.Id, institucion.Nombre, inst.Nombre, listaCorreos, false, false, true);

                        //cr.Enviar(mnsj);
                        var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
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

    }
}