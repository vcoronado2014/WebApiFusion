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
using System.IO;
using System.Text;
using System.Web;

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TricelController : ApiController
    {

        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

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

                List<VCFramework.Entidad.Tricel> triceles = VCFramework.NegocioMySQL.Tricel.ObtenerTricelPorInstIdTodos(instIdBuscar);

                if (buscarNombreUsuario != "")
                {
                    if (VCFramework.NegocioMySQL.Utiles.IsNumeric(buscarNombreUsuario))
                        triceles = triceles.FindAll(p => p.Id == int.Parse(buscarNombreUsuario));
                    else
                        triceles = triceles.FindAll(p => p.Nombre.ToUpper().Replace(" ", "") == buscarNombreUsuario.ToUpper().Replace(" ", ""));
                }


                if (triceles != null && triceles.Count > 0)
                {
                    foreach (VCFramework.Entidad.Tricel tri in triceles)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = tri.Id;
                        us.NombreCompleto = tri.Objetivo;
                        us.NombreUsuario = tri.Nombre;

                        us.OtroUno = DateTime.Parse(tri.FechaInicio, culture).ToShortDateString() ;
                        us.OtroDos = DateTime.Parse(tri.FechaTermino, culture).ToShortDateString();
                        us.OtroTres = tri.FechaCreacion.ToShortDateString();
                        //cantidad de listas asociadas al tricel
                        List<VCFramework.Entidad.ListaTricel> listas = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaTricelPorTricelId(tri.Id);
                        us.OtroCuatro = listas.Count.ToString();
                        us.OtroCinco = us.OtroUno + " - " + us.OtroDos;
                        //us.UrlDocumento = insti.UrlDocumento;

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
                        tricel.FechaInicio = DateTime.Parse(fechaInicio, culture).ToShortDateString();
                        tricel.FechaTermino = DateTime.Parse(fechaTermino, culture).ToShortDateString();
                        tricel.Nombre = nombre;
                        tricel.Objetivo = objetivo;
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
                    }
                }
                else
                {
                    //es nuevo
                    tricel.Eliminado = 0;
                    tricel.EsVigente = 1;
                    tricel.FechaInicio = DateTime.Parse(fechaInicio, culture).ToShortDateString();
                    tricel.FechaTermino = DateTime.Parse(fechaTermino, culture).ToShortDateString();
                    tricel.Nombre = nombre;
                    tricel.Objetivo = objetivo;
                    tricel.InstId = int.Parse(instId);
                    tricel.UsuIdCreador = int.Parse(usuId);
                    tricel.FechaCreacion = DateTime.Now;
                    tricel.Id = VCFramework.NegocioMySQL.Tricel.Insertar(tricel);
                    #region manejo del evento
                    //ojo acá, hay que modificar el evento también
                    //para este caso vamos a utilizar el tipo 3 que será Tricel
                    //el Status contendrá el id del elemento original
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
                    List<VCFramework.Entidad.Calendario> listaCalendario = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstidTipo(inst.InstId, 3);
                    if (listaCalendario != null && listaCalendario.Count > 0)
                    {

                        listaCalendario = listaCalendario.FindAll(p => p.Status == inst.Id);
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