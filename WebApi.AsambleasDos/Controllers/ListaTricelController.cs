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
    public class ListaTricelController : ApiController
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
                string triId = data.TriId;
                int triIdBuscar = int.Parse(triId);

                VCFramework.EntidadFuncional.proposalss votaciones = new VCFramework.EntidadFuncional.proposalss();
                votaciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.ListaTricel> triceles = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaTricelPorTricelId(triIdBuscar);

                if (buscarNombreUsuario != "")
                {
                    if (VCFramework.NegocioMySQL.Utiles.IsNumeric(buscarNombreUsuario))
                        triceles = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaTricelPorId(int.Parse(buscarNombreUsuario));
                    else
                        triceles = triceles.FindAll(p => p.Nombre == buscarNombreUsuario);
                }


                if (triceles != null && triceles.Count > 0)
                {
                    foreach (VCFramework.Entidad.ListaTricel tri in triceles)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = tri.Id;
                        us.NombreCompleto = tri.Objetivo;
                        us.NombreUsuario = tri.Nombre;

                        us.OtroUno = tri.FechaInicio;
                        us.OtroDos = tri.FechaTermino;

                        us.OtroTres = tri.Descripcion;
                        us.OtroCuatro = tri.Beneficios;
                        //buscamos la informaciòn del tricel
                        List<VCFramework.Entidad.Tricel> padre = VCFramework.NegocioMySQL.Tricel.ObtenerTricelPorId(tri.TriId);
                        if (padre != null && padre.Count == 1)
                        {
                            VCFramework.Entidad.Tricel tricelMostrar = padre[0];
                            StringBuilder sb = new StringBuilder();
                            /*
                             * 
                             * 
                             * 
                                <div class='col-xs-12 page-header'><h5>Info <small>del Tricel</small></h5></div>

                            */
                            //sb.Append("<div class='col-xs-12 page-header'><h5>Info <small>del Tricel</small></h5></div>");
                            //sb.Append("\r\n");
                            sb.AppendFormat("Nombre del Tricel {0}", tricelMostrar.Nombre);
                            sb.Append(" --- ");
                            sb.AppendFormat("Objetivo del Tricel: {0}", tricelMostrar.Objetivo);
                            us.OtroCinco = sb.ToString();
                        }
                        //us.UrlDocumento = insti.UrlDocumento;

                        us.Url = "CrearModificarListaTricel.html?id=" + us.Id.ToString() + "&ELIMINAR=0&triId=" + tri.TriId.ToString();
                        us.UrlEliminar = "CrearModificarListaTricel.html?id=" + us.Id.ToString() + "&ELIMINAR=1&triId=" + tri.TriId.ToString();


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


                VCFramework.Entidad.ListaTricel inst = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaTricelPorId(idBuscar)[0];

                if (inst != null && inst.Id > 0)
                {
                    inst.Eliminado = 1;


                    VCFramework.NegocioMySQL.ListaTricel.Eliminar(inst);

                    List<VCFramework.Entidad.UsuarioLista> usuariosLista = VCFramework.NegocioMySQL.UsuarioLista.Obtener(int.Parse(id));
                    if (usuariosLista != null && usuariosLista.Count > 0)
                    {
                        foreach(VCFramework.Entidad.UsuarioLista usl in usuariosLista)
                        {
                            VCFramework.NegocioMySQL.UsuarioLista.Eliminar(usl);
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

            string id = data.Id;
            if (id == null)
                id = "0";

            string instId = data.InstId;
            string nombre = data.Nombre;
            string objetivo = data.Objetivo;
            string fechaInicio = data.FechaInicio;
            string fechaTermino = data.FechaTermino;
            string usuId = data.IdUsuario;
            string descripcion = data.Descripcion;
            string beneficios = data.Beneficios;
            string triId = data.TriId;
            string rolId = data.RolId;

            string usuIdPresidente = data.UsuIdPresidente;
            string usuIdVicePresidente = data.UsuIdVice;
            string usuIdTesorero = data.UsuIdTesorero;
            string usuIdSecretario = data.UsuIdSecretario;
            string usuIdOtroUno = data.UsuIdOtroUno;
            string usuIdOtroDos = data.UsuIdOtroDos;
            string usuIdOtroTres = data.UsuIdOtroTres;
            string usuIdOtroCuatro = data.UsuIdOtroCuatro;
            string usuIdOtroCinco = data.UsuIdOtroCinco;



            HttpResponseMessage httpResponse = new HttpResponseMessage();
            int idNuevo = 0;

            VCFramework.Entidad.Tricel tricelLista = new VCFramework.Entidad.Tricel();

            try
            {
                VCFramework.Entidad.ListaTricel tricel = new VCFramework.Entidad.ListaTricel();
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");
                IFormatProvider culture1 = new System.Globalization.CultureInfo("es-CL", true);

                List<VCFramework.Entidad.Tricel> tricelesLista = VCFramework.NegocioMySQL.Tricel.ObtenerTricelPorId(int.Parse(triId));
                if (tricelesLista != null && tricelesLista.Count == 1)
                    tricelLista = tricelesLista[0];

                if (id != "0")
                {
                    //es modificado
                    List<VCFramework.Entidad.ListaTricel> triceles = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaTricelPorId(int.Parse(id));
                    if (triceles.Count == 1)
                    {
                        tricel = triceles[0];
                        tricel.FechaInicio = tricelLista.FechaInicio;
                        tricel.FechaTermino = tricelLista.FechaTermino;
                        tricel.Nombre = nombre;
                        tricel.Objetivo = objetivo;
                        tricel.Descripcion = descripcion;
                        tricel.Beneficios = beneficios;
                        VCFramework.NegocioMySQL.ListaTricel.Modificar(tricel);

                    }
                    ///agrgar los cambios para que pueda soportar la inserciòn de los usuarios listas
                    ///
                    #region listas de usuario
                    //verificamos si existen listas de usuario
                    List<VCFramework.Entidad.UsuarioLista> usuariosLista = VCFramework.NegocioMySQL.UsuarioLista.Obtener(int.Parse(id));
                    if (usuariosLista != null && usuariosLista.Count > 0)
                    {
                        //presidente
                        VCFramework.Entidad.UsuarioLista uslPresidente = usuariosLista.Find(p => p.Rol == "Presidente");
                        if (uslPresidente != null && uslPresidente.Id > 0 && data.UsuIdPresidente != null)
                        {
                            uslPresidente.UsuId = int.Parse(usuIdPresidente);
                            VCFramework.NegocioMySQL.UsuarioLista.Modificar(uslPresidente);
                        }
                        else
                        {
                            if (data.UsuIdPresidente != null) { 
                            uslPresidente = new VCFramework.Entidad.UsuarioLista();
                            uslPresidente.Eliminado = 0;
                            uslPresidente.LtrId = int.Parse(id);
                            uslPresidente.Rol = "Presidente";
                            uslPresidente.UsuId = int.Parse(usuIdPresidente);
                            VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslPresidente);
                            }
                        }
                        //vicepresidente
                        VCFramework.Entidad.UsuarioLista uslVicePresidente = usuariosLista.Find(p => p.Rol == "VicePresidente");
                        if (uslVicePresidente != null && uslVicePresidente.Id > 0 && data.UsuIdVice != null)
                        {
                            uslVicePresidente.UsuId = int.Parse(usuIdVicePresidente);
                            VCFramework.NegocioMySQL.UsuarioLista.Modificar(uslVicePresidente);
                        }
                        else
                        {
                            if (data.UsuIdVice != null)
                            {
                                uslVicePresidente = new VCFramework.Entidad.UsuarioLista();
                                uslVicePresidente.Eliminado = 0;
                                uslVicePresidente.LtrId = int.Parse(id);
                                uslVicePresidente.Rol = "VicePresidente";
                                uslVicePresidente.UsuId = int.Parse(usuIdVicePresidente);
                                VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslVicePresidente);
                            }
                        }
                        //secretario
                        VCFramework.Entidad.UsuarioLista uslSecretario = usuariosLista.Find(p => p.Rol == "Secretario");
                        if (uslSecretario != null && uslSecretario.Id > 0 && data.UsuIdSecretario != null)
                        {
                            uslSecretario.UsuId = int.Parse(usuIdSecretario);
                            VCFramework.NegocioMySQL.UsuarioLista.Modificar(uslSecretario);
                        }
                        else
                        {
                            if (data.UsuIdSecretario != null)
                            {
                                uslSecretario = new VCFramework.Entidad.UsuarioLista();
                                uslSecretario.Eliminado = 0;
                                uslSecretario.LtrId = int.Parse(id);
                                uslSecretario.Rol = "Secretario";
                                uslSecretario.UsuId = int.Parse(usuIdSecretario);
                                VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslSecretario);
                            }
                        }
                        //tesorero
                        VCFramework.Entidad.UsuarioLista uslTesorero = usuariosLista.Find(p => p.Rol == "Tesorero");
                        if (uslTesorero != null && uslTesorero.Id > 0 && data.UsuIdTesorero != null)
                        {
                            uslTesorero.UsuId = int.Parse(usuIdTesorero);
                            VCFramework.NegocioMySQL.UsuarioLista.Modificar(uslTesorero);
                        }
                        else
                        {
                            if (data.UsuIdTesorero != null)
                            {
                                uslTesorero = new VCFramework.Entidad.UsuarioLista();
                                uslTesorero.Eliminado = 0;
                                uslTesorero.LtrId = int.Parse(id);
                                uslTesorero.Rol = "Tesorero";
                                uslTesorero.UsuId = int.Parse(usuIdTesorero);
                                VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslTesorero);
                            }
                        }
                        //OtroUno
                        VCFramework.Entidad.UsuarioLista uslOtroUno = usuariosLista.Find(p => p.Rol == "OtroUno");
                        if (uslOtroUno != null && uslOtroUno.Id > 0 && data.UsuIdOtroUno != null)
                        {
                            uslOtroUno.UsuId = int.Parse(usuIdOtroUno);
                            VCFramework.NegocioMySQL.UsuarioLista.Modificar(uslOtroUno);
                        }
                        else
                        {
                            if (data.UsuIdOtroUno != null)
                            {
                                uslOtroUno = new VCFramework.Entidad.UsuarioLista();
                                uslOtroUno.Eliminado = 0;
                                uslOtroUno.LtrId = int.Parse(id);
                                uslOtroUno.Rol = "OtroUno";
                                uslOtroUno.UsuId = int.Parse(usuIdOtroUno);
                                VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroUno);
                            }
                        }
                        //OtroDos
                        VCFramework.Entidad.UsuarioLista uslOtroDos = usuariosLista.Find(p => p.Rol == "OtroDos");
                        if (uslOtroDos != null && uslOtroDos.Id > 0 && data.UsuIdOtroDos != null)
                        {
                            uslOtroDos.UsuId = int.Parse(usuIdOtroDos);
                            VCFramework.NegocioMySQL.UsuarioLista.Modificar(uslOtroDos);
                        }
                        else
                        {
                            if (data.UsuIdOtroDos != null)
                            {
                                uslOtroDos = new VCFramework.Entidad.UsuarioLista();
                                uslOtroDos.Eliminado = 0;
                                uslOtroDos.LtrId = int.Parse(id);
                                uslOtroDos.Rol = "OtroDos";
                                uslOtroDos.UsuId = int.Parse(usuIdOtroDos);
                                VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroDos);
                            }
                        }
                        //OtroTres
                        VCFramework.Entidad.UsuarioLista uslOtroTres = usuariosLista.Find(p => p.Rol == "OtroTres");
                        if (uslOtroTres != null && uslOtroTres.Id > 0 && data.UsuIdOtroTres != null)
                        {
                            uslOtroTres.UsuId = int.Parse(usuIdOtroTres);
                            VCFramework.NegocioMySQL.UsuarioLista.Modificar(uslOtroTres);
                        }
                        else
                        {
                            if (data.UsuIdOtroTres != null)
                            {
                                uslOtroTres = new VCFramework.Entidad.UsuarioLista();
                                uslOtroTres.Eliminado = 0;
                                uslOtroTres.LtrId = int.Parse(id);
                                uslOtroTres.Rol = "OtroTres";
                                uslOtroTres.UsuId = int.Parse(usuIdOtroTres);
                                VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroTres);
                            }
                        }
                        //OtroCuatro
                        VCFramework.Entidad.UsuarioLista uslOtroCuatro = usuariosLista.Find(p => p.Rol == "OtroCuatro");
                        if (uslOtroCuatro != null && uslOtroCuatro.Id > 0 && data.UsuIdOtroCuatro != null)
                        {
                            uslOtroCuatro.UsuId = int.Parse(usuIdOtroCuatro);
                            VCFramework.NegocioMySQL.UsuarioLista.Modificar(uslOtroCuatro);
                        }
                        else
                        {
                            if (data.UsuIdOtroCuatro != null)
                            {
                                uslOtroCuatro = new VCFramework.Entidad.UsuarioLista();
                                uslOtroCuatro.Eliminado = 0;
                                uslOtroCuatro.LtrId = int.Parse(id);
                                uslOtroCuatro.Rol = "OtroCuatro";
                                uslOtroCuatro.UsuId = int.Parse(usuIdOtroCuatro);
                                VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroCuatro);
                            }
                        }
                        //OtroCinco
                        VCFramework.Entidad.UsuarioLista uslOtroCinco = usuariosLista.Find(p => p.Rol == "OtroCinco");
                        if (uslOtroCinco != null && uslOtroCinco.Id > 0 && data.UsuIdOtroCinco != null)
                        {
                            uslOtroCinco.UsuId = int.Parse(usuIdOtroCinco);
                            VCFramework.NegocioMySQL.UsuarioLista.Modificar(uslOtroCinco);
                        }
                        else
                        {
                            if (data.UsuIdOtroCinco != null)
                            {
                                uslOtroCinco = new VCFramework.Entidad.UsuarioLista();
                                uslOtroCinco.Eliminado = 0;
                                uslOtroCinco.LtrId = int.Parse(id);
                                uslOtroCinco.Rol = "OtroCinco";
                                uslOtroCinco.UsuId = int.Parse(usuIdOtroCinco);
                                VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroCinco);
                            }
                        }

                    }
                    else
                    {
                        #region listas de usuario
                        //verificamos si existen listas de usuario
                        //presidente
                        if (data.UsuIdPresidente != null)
                        {
                            VCFramework.Entidad.UsuarioLista uslPresidente = new VCFramework.Entidad.UsuarioLista();

                            uslPresidente.Eliminado = 0;
                            uslPresidente.LtrId = int.Parse(id);
                            uslPresidente.Rol = "Presidente";
                            uslPresidente.UsuId = int.Parse(usuIdPresidente);
                            VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslPresidente);
                        }
                        if (data.UsuIdVice != null)
                        {
                            //vicepresidente
                            VCFramework.Entidad.UsuarioLista uslVicePresidente = new VCFramework.Entidad.UsuarioLista();

                            uslVicePresidente.Eliminado = 0;
                            uslVicePresidente.LtrId = int.Parse(id);
                            uslVicePresidente.Rol = "VicePresidente";
                            uslVicePresidente.UsuId = int.Parse(usuIdVicePresidente);
                            VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslVicePresidente);
                        }
                        if (data.UsuIdSecretario != null)
                        {
                            //secretario
                            VCFramework.Entidad.UsuarioLista uslSecretario = new VCFramework.Entidad.UsuarioLista();

                            uslSecretario.Eliminado = 0;
                            uslSecretario.LtrId = int.Parse(id);
                            uslSecretario.Rol = "Secretario";
                            uslSecretario.UsuId = int.Parse(usuIdSecretario);
                            VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslSecretario);
                        }
                        if (data.UsuIdTesorero != null)
                        {
                            //tesorero
                            VCFramework.Entidad.UsuarioLista uslTesorero = new VCFramework.Entidad.UsuarioLista();

                            uslTesorero.Eliminado = 0;
                            uslTesorero.LtrId = int.Parse(id);
                            uslTesorero.Rol = "Tesorero";
                            uslTesorero.UsuId = int.Parse(usuIdTesorero);
                            VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslTesorero);
                        }
                        if (data.UsuIdOtroUno != null)
                        {
                            //OtroUno
                            VCFramework.Entidad.UsuarioLista uslOtroUno = new VCFramework.Entidad.UsuarioLista();

                            uslOtroUno.Eliminado = 0;
                            uslOtroUno.LtrId = int.Parse(id);
                            uslOtroUno.Rol = "OtroUno";
                            uslOtroUno.UsuId = int.Parse(usuIdOtroUno);
                            VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroUno);
                        }
                        if (data.UsuIdOtroDos != null)
                        {
                            //OtroDos
                            VCFramework.Entidad.UsuarioLista uslOtroDos = new VCFramework.Entidad.UsuarioLista();

                            uslOtroDos.Eliminado = 0;
                            uslOtroDos.LtrId = int.Parse(id);
                            uslOtroDos.Rol = "OtroDos";
                            uslOtroDos.UsuId = int.Parse(usuIdOtroDos);
                            VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroDos);
                        }
                        if (data.UsuIdOtroTres != null)
                        {
                            //OtroTres
                            VCFramework.Entidad.UsuarioLista uslOtroTres = new VCFramework.Entidad.UsuarioLista();

                            uslOtroTres.Eliminado = 0;
                            uslOtroTres.LtrId = int.Parse(id);
                            uslOtroTres.Rol = "OtroTres";
                            uslOtroTres.UsuId = int.Parse(usuIdOtroTres);
                            VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroTres);
                        }
                        if (data.UsuIdOtroCuatro != null)
                        {
                            //OtroCuatro
                            VCFramework.Entidad.UsuarioLista uslOtroCuatro = new VCFramework.Entidad.UsuarioLista();

                            uslOtroCuatro.Eliminado = 0;
                            uslOtroCuatro.LtrId = int.Parse(id);
                            uslOtroCuatro.Rol = "OtroCuatro";
                            uslOtroCuatro.UsuId = int.Parse(usuIdOtroCuatro);
                            VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroCuatro);
                        }
                        if (data.UsuIdOtroCinco != null)
                        {
                            //OtroCinco
                            VCFramework.Entidad.UsuarioLista uslOtroCinco = new VCFramework.Entidad.UsuarioLista();

                            uslOtroCinco.Eliminado = 0;
                            uslOtroCinco.LtrId = int.Parse(id);
                            uslOtroCinco.Rol = "OtroCinco";
                            uslOtroCinco.UsuId = int.Parse(usuIdOtroCinco);
                            VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroCinco);

                        }


                        #endregion
                    }

                    #endregion
                }
                else
                {
                    //es nuevo
                    tricel.Eliminado = 0;
                    tricel.FechaInicio = tricelLista.FechaInicio;
                    tricel.FechaTermino = tricelLista.FechaTermino;
                    tricel.Nombre = nombre;
                    tricel.Objetivo = objetivo;
                    tricel.InstId = int.Parse(instId);
                    tricel.Descripcion = descripcion;
                    tricel.Beneficios = beneficios;
                    tricel.Rol = rolId;
                    tricel.TriId = int.Parse(triId);
                    tricel.UsuId = int.Parse(usuId);
                    tricel.Id = VCFramework.NegocioMySQL.ListaTricel.Insertar(tricel);
                    id = tricel.Id.ToString();

                    ///agrgar los cambios para que pueda soportar la inserciòn de los usuarios listas
                    ///
                    #region listas de usuario
                    //verificamos si existen listas de usuario
                    //presidente
                    if (data.UsuIdPresidente != null)
                    {
                        VCFramework.Entidad.UsuarioLista uslPresidente = new VCFramework.Entidad.UsuarioLista();

                        uslPresidente.Eliminado = 0;
                        uslPresidente.LtrId = int.Parse(id);
                        uslPresidente.Rol = "Presidente";
                        uslPresidente.UsuId = int.Parse(usuIdPresidente);
                        VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslPresidente);
                    }
                    if (data.UsuIdVice != null)
                    {
                        //vicepresidente
                        VCFramework.Entidad.UsuarioLista uslVicePresidente = new VCFramework.Entidad.UsuarioLista();

                        uslVicePresidente.Eliminado = 0;
                        uslVicePresidente.LtrId = int.Parse(id);
                        uslVicePresidente.Rol = "VicePresidente";
                        uslVicePresidente.UsuId = int.Parse(usuIdVicePresidente);
                        VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslVicePresidente);
                    }
                    if (data.UsuIdSecretario != null)
                    {
                        //secretario
                        VCFramework.Entidad.UsuarioLista uslSecretario = new VCFramework.Entidad.UsuarioLista();

                        uslSecretario.Eliminado = 0;
                        uslSecretario.LtrId = int.Parse(id);
                        uslSecretario.Rol = "Secretario";
                        uslSecretario.UsuId = int.Parse(usuIdSecretario);
                        VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslSecretario);
                    }
                    if (data.UsuIdTesorero != null)
                    {
                        //tesorero
                        VCFramework.Entidad.UsuarioLista uslTesorero = new VCFramework.Entidad.UsuarioLista();

                        uslTesorero.Eliminado = 0;
                        uslTesorero.LtrId = int.Parse(id);
                        uslTesorero.Rol = "Tesorero";
                        uslTesorero.UsuId = int.Parse(usuIdTesorero);
                        VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslTesorero);
                    }
                    if (data.UsuIdOtroUno != null)
                    {
                        //OtroUno
                        VCFramework.Entidad.UsuarioLista uslOtroUno = new VCFramework.Entidad.UsuarioLista();

                        uslOtroUno.Eliminado = 0;
                        uslOtroUno.LtrId = int.Parse(id);
                        uslOtroUno.Rol = "OtroUno";
                        uslOtroUno.UsuId = int.Parse(usuIdOtroUno);
                        VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroUno);
                    }
                    if (data.UsuIdOtroDos != null)
                    {
                        //OtroDos
                        VCFramework.Entidad.UsuarioLista uslOtroDos = new VCFramework.Entidad.UsuarioLista();

                        uslOtroDos.Eliminado = 0;
                        uslOtroDos.LtrId = int.Parse(id);
                        uslOtroDos.Rol = "OtroDos";
                        uslOtroDos.UsuId = int.Parse(usuIdOtroDos);
                        VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroDos);
                    }
                    if (data.UsuIdOtroTres != null)
                    {
                        //OtroTres
                        VCFramework.Entidad.UsuarioLista uslOtroTres = new VCFramework.Entidad.UsuarioLista();

                        uslOtroTres.Eliminado = 0;
                        uslOtroTres.LtrId = int.Parse(id);
                        uslOtroTres.Rol = "OtroTres";
                        uslOtroTres.UsuId = int.Parse(usuIdOtroTres);
                        VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroTres);
                    }
                    if (data.UsuIdOtroCuatro != null)
                    {
                        //OtroCuatro
                        VCFramework.Entidad.UsuarioLista uslOtroCuatro = new VCFramework.Entidad.UsuarioLista();

                        uslOtroCuatro.Eliminado = 0;
                        uslOtroCuatro.LtrId = int.Parse(id);
                        uslOtroCuatro.Rol = "OtroCuatro";
                        uslOtroCuatro.UsuId = int.Parse(usuIdOtroCuatro);
                        VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroCuatro);
                    }
                    if (data.UsuIdOtroCinco != null)
                    {
                        //OtroCinco
                        VCFramework.Entidad.UsuarioLista uslOtroCinco = new VCFramework.Entidad.UsuarioLista();

                        uslOtroCinco.Eliminado = 0;
                        uslOtroCinco.LtrId = int.Parse(id);
                        uslOtroCinco.Rol = "OtroCinco";
                        uslOtroCinco.UsuId = int.Parse(usuIdOtroCinco);
                        VCFramework.NegocioMySQL.UsuarioLista.Insertar(uslOtroCinco);

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

    }
}