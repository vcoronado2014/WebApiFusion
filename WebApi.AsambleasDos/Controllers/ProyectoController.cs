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

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProyectoController : ApiController
    {
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

                VCFramework.EntidadFuncional.proposalss votaciones = new VCFramework.EntidadFuncional.proposalss();
                votaciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.Proyectos> triceles = VCFramework.NegocioMySQL.Proyectos.ObtenerProyectosPorInstIdTodos(instIdBuscar);

                if (buscarNombreUsuario != "")
                {
                    if (VCFramework.NegocioMySQL.Utiles.IsNumeric(buscarNombreUsuario))
                        triceles = triceles.FindAll(p => p.Id == int.Parse(buscarNombreUsuario));
                    else
                        triceles = triceles.FindAll(p => p.Nombre == buscarNombreUsuario);
                }


                if (triceles != null && triceles.Count > 0)
                {
                    foreach (VCFramework.Entidad.Proyectos tri in triceles)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = tri.Id;
                        us.NombreCompleto = tri.Objetivo;
                        us.NombreUsuario = tri.Nombre;

                        us.OtroUno = DateTime.Parse(tri.FechaInicio.ToShortDateString(), culture).ToShortDateString().Replace("/", "-");
                        us.OtroDos = DateTime.Parse(tri.FechaTermino.ToShortDateString(), culture).ToShortDateString().Replace("/", "-");
                        us.OtroTres = tri.FechaCreacion.ToShortDateString();

                        us.OtroCinco = us.OtroUno + " - " + us.OtroDos;

                        us.Url = "CrearModificarProyecto.html?id=" + us.Id.ToString() + "&ELIMINAR=0";
                        us.UrlEliminar = "CrearModificarProyecto.html?id=" + us.Id.ToString() + "&ELIMINAR=1";


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
            string costo = data.Costo;



            HttpResponseMessage httpResponse = new HttpResponseMessage();
            int idNuevo = 0;



            try
            {
                VCFramework.Entidad.Proyectos tricel = new VCFramework.Entidad.Proyectos();
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");
                IFormatProvider culture1 = new System.Globalization.CultureInfo("es-CL", true);
                if (id != "0")
                {
                    //es modificado
                    List<VCFramework.Entidad.Proyectos> triceles = VCFramework.NegocioMySQL.Proyectos.ObtenerProyectosPorId(int.Parse(id));
                    if (triceles.Count == 1)
                    {
                        tricel = triceles[0];
                        tricel.FechaInicio = DateTime.Parse(fechaInicio, culture);
                        tricel.FechaTermino = DateTime.Parse(fechaTermino, culture);
                        tricel.Nombre = nombre;
                        tricel.Objetivo = objetivo;
                        tricel.Costo = int.Parse(costo);
                        VCFramework.NegocioMySQL.Proyectos.Modificar(tricel);
                        
                    }
                }
                else
                {
                    //es nuevo
                    tricel.Eliminado = 0;
                    tricel.EsVigente = 1;
                    tricel.FechaInicio = DateTime.Parse(fechaInicio, culture);
                    tricel.FechaTermino = DateTime.Parse(fechaTermino, culture);
                    tricel.Nombre = nombre;
                    tricel.Objetivo = objetivo;
                    tricel.InstId = int.Parse(instId);
                    tricel.UsuIdCreador = int.Parse(usuId);
                    tricel.Costo = int.Parse(costo);
                    tricel.EsVigente = 1;
                    tricel.FueAprobado = 1;
                    tricel.NotificaPopup = 1;
                    tricel.FechaCreacion = DateTime.Now;
                    tricel.Id = VCFramework.NegocioMySQL.Proyectos.Insertar(tricel);
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


                VCFramework.Entidad.Proyectos inst = VCFramework.NegocioMySQL.Proyectos.ObtenerProyectosPorId(idBuscar)[0];

                if (inst != null && inst.Id > 0)
                {
                    inst.Eliminado = 1;


                    VCFramework.NegocioMySQL.Proyectos.Modificar(inst);
                    //archivos tricel
                    List<VCFramework.Entidad.ArchivosProyecto> archivos = VCFramework.NegocioMySQL.ArchivosProyecto.ObtenerArchivosPorProyectoId(idBuscar, null);
                    if (archivos != null && archivos.Count > 0)
                    {
                        foreach (VCFramework.Entidad.ArchivosProyecto arc in archivos)
                        {
                            VCFramework.NegocioMySQL.ArchivosProyecto.Eliminar(arc);
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
    }
}