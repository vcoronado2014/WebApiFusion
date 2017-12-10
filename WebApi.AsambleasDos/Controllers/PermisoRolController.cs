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
using System.Net.Mail;

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PermisoRolController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [AgendaExceptionFilter]
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]string instId, [FromUri]string rolId)
        {

            if (instId == "")
                throw new ArgumentNullException("InstitucionId");
            if (rolId == "")
                throw new ArgumentNullException("rolId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                List<VCFramework.Entidad.PermisoRol> permisos = new List<PermisoRol>();
                //string instId = data.InstId;
                int idInstitucion = int.Parse(instId);
                int rolIdBuscar = int.Parse(rolId);
                //evaluamos el rol
                permisos = VCFramework.NegocioMySql.PermisoRol.ObtenerPermisoRolPorRolId(rolIdBuscar, idInstitucion);

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(permisos);
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

        [AgendaExceptionFilter]
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]string instId)
        {

            if (instId == "")
                throw new ArgumentNullException("InstitucionId");

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                List<VCFramework.Entidad.PermisoRol> permisos = new List<PermisoRol>();
                //string instId = data.InstId;
                int idInstitucion = int.Parse(instId);
                //evaluamos el rol
                permisos = VCFramework.NegocioMySql.PermisoRol.ObtenerPermisoRolPorInstId(idInstitucion);

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(permisos);
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
            if (data.Id == 0)
                throw new ArgumentNullException("Id");

            VCFramework.Entidad.PermisoRol permiso = new PermisoRol();

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string id = data.Id;
                int idBuscar = int.Parse(id);

                permiso = VCFramework.NegocioMySql.PermisoRol.ObtenerPermisoRolPorId(idBuscar);

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(permiso);
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
            /*
            CreaCalendario
:
true
CreaDocumento
:
true
CreaIntitucion
:
true
CreaMuro
:
true
CreaProyecto
:
true
CreaRendicion
:
false
CreaRol
:
true
CreaTricel
:
true
CreaUsuario
:
true
Descripcion
:
"Super"
EliminaCalendario
:
true
EliminaDocumento
:
true
EliminaInstitucion
:
true
EliminaMuro
:
true
EliminaProyecto
:
true
EliminaRendicion
:
false
EliminaRol
:
true
EliminaTricel
:
true
EliminaUsuario
:
true
Id
:
"1"
InstId
:
3
ModificaCalendario
:
true
ModificaInstitucion
:
true
ModificaMuro
:
true
ModificaProyecto
:
true
ModificaRendicion
:
false
ModificaRol
:
true
ModificaTricel
:
true
ModificaUsuario
:
true
Nombre
:
"Super Administrador"
PermisoId
:
1
PuedeVotarProyecto
:
true
PuedeVotarTricel
:
true
VerCalendario
:
true
VerDocumento
:
true
VerInstitucion
:
true
VerMuro
:
true
VerProyecto
:
true
VerRendicion
:
true
VerRol
:
true
VerTricel
:
true
VerUsuario
:
true 
            */

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string id = data.Id;
            string instId = data.InstId;
            string rolId = data.Id;
            string nombre = data.Nombre;
            string descripcion = data.Descripcion;
            string permisoId = data.PermisoId;
            string creaUsuario = data.CreaUsuario;
            string modificaUsuario = data.ModificaUsuario;
            string eliminaUsuario = data.EliminaUsuario;
            string verUsuario = data.VerUsuario;
            string creaInstitucion = data.CreaInstitucion;
            string modificaInstitucion = data.ModificaInstitucion;
            string eliminaInstitucion = data.EliminaInstitucion;
            string verInstitucion = data.VerInstitucion;
            string creaDocumento = data.CreaDocumento;
            string eliminaDocumento = data.EliminaDocumento;
            string verDocumento = data.VerDocumento;
            string creaCalendario = data.CreaCalendario;
            string modificaCalendario = data.ModificaCalendario;
            string eliminaCalendario = data.EliminaCalendario;
            string verCalendario = data.VerCalendario;
            string creaTricel = data.CreaTricel;
            string modificaTricel = data.ModificaTricel;
            string eliminaTricel = data.EliminaTricel;
            string verTricel = data.VerTricel;
            string creaProyecto = data.CreaProyecto;
            string modificaProyecto = data.ModificaProyecto;
            string eliminaProyecto = data.EliminaProyecto;
            string verProyecto = data.VerProyecto;
            string creaRendicion = data.CreaRendicion;
            string modificaRendicion = data.ModificaRendicion;
            string eliminaRendicion = data.EliminaRendicion;
            string verRendicion = data.VerRendicion;
            string creaRol = data.CreaRol;
            string modificaRol = data.ModificaRol;
            string eliminaRol = data.EliminaRol;
            string verRol = data.VerRol;
            string creaMuro = data.CreaMuro;
            string modificaMuro = data.ModificaMuro;
            string eliminaMuro = data.EliminaMuro;
            string verMuro = data.VerMuro;
            string puedeVotarProyecto = data.PuedeVotarProyecto;
            string puedeVotarTricel = data.PuedeVotarTricel;
            string verMailing = data.VerMailing;
            string creaMailing = data.CreaMailing;
            string verReportes = data.VerReportes;
            string verReporteAsistencia = data.VerReporteAsistencia;
            //nuevos
            string creaSolicitud = data.CreaSolicitudes;
            string creaMroSolicitud = data.CreaMroSolicitudes;
            string modificaMroSolicitud = data.ModificaMroSolicitudes;
            string eliminaMroSolicitud = data.EliminaMroSolicitudes;
            string verMroSolicitud = data.VerMroSolicitudes;
            //***************************************************
            int idNuevo = 0;
            int idNuevoRol = 0;

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
                VCFramework.Entidad.RolInstitucion rolGuardar = new RolInstitucion();
                rolGuardar.Descripcion = descripcion;
                rolGuardar.Id = int.Parse(rolId);
                rolGuardar.IdOriginal = 0;
                rolGuardar.InstId = int.Parse(instId);
                rolGuardar.Nombre = nombre;
                if (rolGuardar.Id > 0)
                {
                    VCFramework.NegocioMySql.RolInstitucion.Modificar(rolGuardar);
                    idNuevoRol = rolGuardar.Id;

                    List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(rolGuardar.InstId);
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
                        VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(rolGuardar.InstId);
                        VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                        //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, false);
                        MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeRol(institucion.Id, institucion.Nombre, rolGuardar.Nombre, listaCorreos, false, true, false, esCpas);

                        //cr.Enviar(mnsj);
                        var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                    }

                }
                else
                {
                    idNuevoRol = VCFramework.NegocioMySql.RolInstitucion.Insertar(rolGuardar);
                    rolGuardar.Id = idNuevoRol;

                    List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(rolGuardar.InstId);
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
                        VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(rolGuardar.InstId);
                        VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                        //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, false);
                        MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeRol(institucion.Id, institucion.Nombre, rolGuardar.Nombre, listaCorreos, true, false, false, esCpas);

                        //cr.Enviar(mnsj);
                        var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                    }

                }

                VCFramework.Entidad.PermisoRol permiso = new PermisoRol();
                //entonces vemos si modifica o crea
                
                permiso.Id = int.Parse(permisoId);
                permiso.InstId = int.Parse(instId);
                permiso.CreaCalendario = Convert.ToBoolean(creaCalendario) ? 1 : 0;
                //int.Parse(creaCalendario);
                permiso.CreaDocumento = Convert.ToBoolean(creaDocumento) ? 1 : 0;
                permiso.CreaInstitucion = Convert.ToBoolean(creaInstitucion) ? 1 : 0;
                permiso.CreaMuro = Convert.ToBoolean(creaMuro) ? 1 : 0;
                permiso.CreaProyecto = Convert.ToBoolean(creaProyecto) ? 1 : 0;
                permiso.CreaRendicion = Convert.ToBoolean(creaRendicion) ? 1 : 0;
                permiso.CreaRol = Convert.ToBoolean(creaRol) ? 1 : 0;
                permiso.CreaTricel = Convert.ToBoolean(creaTricel) ? 1 : 0;
                permiso.CreaUsuario = Convert.ToBoolean(creaUsuario) ? 1 : 0;
                permiso.EliminaCalendario = Convert.ToBoolean(eliminaCalendario) ? 1 : 0;
                permiso.EliminaDocumento = Convert.ToBoolean(eliminaDocumento) ? 1 : 0;
                permiso.EliminaInstitucion = Convert.ToBoolean(eliminaInstitucion) ? 1 : 0;
                permiso.EliminaMuro = Convert.ToBoolean(eliminaMuro) ? 1 : 0;
                permiso.EliminaProyecto = Convert.ToBoolean(eliminaProyecto) ? 1 : 0;
                permiso.EliminaRendicion = Convert.ToBoolean(eliminaRendicion) ? 1 : 0;
                permiso.EliminaRol = Convert.ToBoolean(eliminaRol) ? 1 : 0;
                permiso.EliminaTricel = Convert.ToBoolean(eliminaTricel) ? 1 : 0;
                permiso.EliminaUsuario = Convert.ToBoolean(eliminaUsuario) ? 1 : 0;
                permiso.ModificaCalendario = Convert.ToBoolean(modificaCalendario) ? 1 : 0;
                permiso.ModificaInstitucion = Convert.ToBoolean(modificaInstitucion) ? 1 : 0;
                permiso.ModificaMuro = Convert.ToBoolean(modificaMuro) ? 1 : 0;
                permiso.ModificaProyecto = Convert.ToBoolean(modificaProyecto) ? 1 : 0;
                permiso.ModificaRendicion = Convert.ToBoolean(modificaRendicion) ? 1 : 0;
                permiso.ModificaRol = Convert.ToBoolean(modificaRol) ? 1 : 0;
                permiso.ModificaTricel = Convert.ToBoolean(modificaTricel) ? 1 : 0;
                permiso.ModificaUsuario = Convert.ToBoolean(modificaUsuario) ? 1 : 0;
                permiso.PuedeVotarProyecto = Convert.ToBoolean(puedeVotarProyecto) ? 1 : 0;
                permiso.PuedeVotarTricel = Convert.ToBoolean(puedeVotarTricel) ? 1 : 0;
                permiso.RolId = idNuevoRol;
                permiso.VerCalendario = Convert.ToBoolean(verCalendario) ? 1 : 0;
                permiso.VerDocumento = Convert.ToBoolean(verDocumento) ? 1 : 0;
                permiso.VerInstitucion = Convert.ToBoolean(verInstitucion) ? 1 : 0;
                permiso.VerMuro = Convert.ToBoolean(verMuro) ? 1 : 0;
                permiso.VerProyecto = Convert.ToBoolean(verProyecto) ? 1 : 0;
                permiso.VerRendicion = Convert.ToBoolean(verRendicion) ? 1 : 0;
                permiso.VerRol = Convert.ToBoolean(verRol) ? 1 : 0;
                permiso.VerTricel = Convert.ToBoolean(verTricel) ? 1 : 0;
                permiso.VerUsuario = Convert.ToBoolean(verUsuario) ? 1 : 0;
                permiso.VerMailing = Convert.ToBoolean(verMailing) ? 1 : 0;
                permiso.CreaMailing = Convert.ToBoolean(creaMailing) ? 1 : 0;
                permiso.VerReportes = Convert.ToBoolean(verReportes) ? 1 : 0;
                permiso.VerReporteAsistencia = Convert.ToBoolean(verReporteAsistencia) ? 1 : 0;
                //nuevos campos
                permiso.CreaSolicitud = Convert.ToBoolean(creaSolicitud) ? 1 : 0;
                permiso.CreaMroSolicitud = Convert.ToBoolean(creaMroSolicitud) ? 1 : 0;
                permiso.ModificaMroSolicitud = Convert.ToBoolean(modificaMroSolicitud) ? 1 : 0;
                permiso.EliminaMroSolicitud = Convert.ToBoolean(eliminaMroSolicitud) ? 1 : 0;
                permiso.VerMroSolicitud = Convert.ToBoolean(verMroSolicitud) ? 1 : 0;

                if (int.Parse(permisoId) > 0)
                {
                    VCFramework.NegocioMySql.PermisoRol.Modificar(permiso);
                }
                else
                {
                    idNuevo = VCFramework.NegocioMySql.PermisoRol.Insertar(permiso);
                    permiso.Id = idNuevo;
                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(permiso);
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

            string idElemento = data.Id;
            string instId = data.InstId;
            //el id corresponde al rol
            if (idElemento == null)
                throw new ArgumentNullException("Id");
            if (instId == null)
                throw new ArgumentNullException("InstId");
            VCFramework.Entidad.RolInstitucion rol = new RolInstitucion();

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            bool puedeBorrar = false;

            try
            {
                //ahora buscamos los usuarios que puedan tener ese rol
                List<VCFramework.EntidadFuncional.UsuarioFuncional> usuarios = VCFramework.NegocioMySQL.AutentificacionUsuario.ListarUsuariosFuncional(int.Parse(instId));
                if (usuarios != null && usuarios.Count > 0)
                {
                    if (usuarios.Exists(p => p.RolInstitucion.Id == int.Parse(idElemento)))
                        puedeBorrar = false;

                    rol.Descripcion = "No se puede eliminar este elemento";

                }
                if (puedeBorrar)
                {
                    rol = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolPorId(int.Parse(idElemento));
                    if (rol != null && rol.Id > 0)
                    {
                        List<VCFramework.Entidad.PermisoRol> permiso = VCFramework.NegocioMySql.PermisoRol.ObtenerPermisoRolPorRolId(rol.Id, int.Parse(instId));
                        if (permiso != null && permiso[0].Id > 0)
                        {
                            VCFramework.NegocioMySql.PermisoRol.Eliminar(permiso[0]);
                        }
                    }
                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(rol);
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