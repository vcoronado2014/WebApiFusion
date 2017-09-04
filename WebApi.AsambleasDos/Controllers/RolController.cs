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
    public class RolController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (data.InstId == 0)
                throw new ArgumentNullException("InstId");

            List<VCFramework.Entidad.RolInstitucion> lista = new List<RolInstitucion>();

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            String JSON = "";
            try
            {
                string idInstitucion = data.InstId;
                int idInstitucionBuscar = int.Parse(idInstitucion);
                List<VCFramework.Entidad.Rol> roles = VCFramework.NegocioMySQL.Rol.ListarRoles();
                List<VCFramework.Entidad.RolInstitucion> rolesInst = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolesPorInstId(idInstitucionBuscar);
                if (roles != null && roles.Count > 0)
                {
                    foreach (VCFramework.Entidad.Rol rolcito in roles)
                    {
                        if (rolesInst.Exists(p=>p.IdOriginal == rolcito.Id))
                        {
                            //si existe hay que ocuparlo
                            lista.Add(rolesInst.FirstOrDefault(p => p.IdOriginal == rolcito.Id));

                        }
                        else
                        {
                            //si no existe se agrega directamente
                            VCFramework.Entidad.RolInstitucion entidad = new RolInstitucion();
                            entidad.Descripcion = rolcito.Descripcion;
                            entidad.Eliminado = rolcito.Eliminado;
                            entidad.Id = rolcito.Id;
                            entidad.IdOriginal = rolcito.Id;
                            entidad.InstId = idInstitucionBuscar;
                            entidad.Nombre = rolcito.Nombre;
                            lista.Add(entidad);

                        }


                    }
                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                JSON = JsonConvert.SerializeObject(lista);
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
        public HttpResponseMessage Get([FromUri]string id, [FromUri]string instId, [FromUri]string idOriginal)
        {
            VCFramework.Entidad.RolInstitucion retoorno = new RolInstitucion();
            if (instId == "")
                throw new ArgumentNullException("InstitucionId");

            //si trae un rol de superadministrador debería listar todos los usuarios
            int idElemento = int.Parse(id);
            int instIdElemento = int.Parse(instId);
            int idOriginalElemento = int.Parse(idOriginal);



            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                List<VCFramework.Entidad.Rol> roles = VCFramework.NegocioMySQL.Rol.ListarRoles();
                List<VCFramework.Entidad.RolInstitucion> rolesInst = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolesPorInstId(instIdElemento);
                //entonces si tiene elementos en los roles privados
                if (rolesInst != null && rolesInst.Count > 0)
                {
                    //buscamos el id
                    if (rolesInst.Exists(p=>p.Id == idElemento))
                    {
                        //el elemento existe
                        retoorno = rolesInst.FirstOrDefault(p => p.Id == idElemento);

                    }
                    else
                    {
                        //no existe el elemento
                        VCFramework.Entidad.Rol rol = roles.FirstOrDefault(p => p.Id == idOriginalElemento);
                        if (rol != null && rol.Id > 0)
                        {
                            retoorno.Descripcion = rol.Descripcion;
                            retoorno.Eliminado = rol.Eliminado;
                            retoorno.Id = rol.Id;
                            retoorno.IdOriginal = rol.Id;
                            retoorno.InstId = instIdElemento;
                            retoorno.Nombre = rol.Nombre;
                        }
                    }
                }
                else
                {
                    //no existe el elemento
                    VCFramework.Entidad.Rol rol = roles.FirstOrDefault(p => p.Id == idOriginalElemento);
                    if (rol != null && rol.Id > 0)
                    {
                        retoorno.Descripcion = rol.Descripcion;
                        retoorno.Eliminado = rol.Eliminado;
                        retoorno.Id = rol.Id;
                        retoorno.IdOriginal = rol.Id;
                        retoorno.InstId = instIdElemento;
                        retoorno.Nombre = rol.Nombre;
                    }
                }
                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(retoorno);
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

            string idElemento = data.Id;
            if (idElemento == null)
                throw new ArgumentNullException("Id");

            string nombreElemento = data.Nombre;
            string descripcionElemento = data.Descrcipcion;
            string instIdElemento = data.InstId;
            string idOriginalElemento = data.IdOriginal;

            VCFramework.Entidad.RolInstitucion retoorno = new RolInstitucion();

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                List<VCFramework.Entidad.Rol> roles = VCFramework.NegocioMySQL.Rol.ListarRoles();
                List<VCFramework.Entidad.RolInstitucion> rolesInst = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolesPorInstId(int.Parse(instIdElemento));
                //entonces si tiene elementos en los roles privados
                if (rolesInst != null && rolesInst.Count > 0)
                {
                    //buscamos el id
                    if (rolesInst.Exists(p => p.Id == int.Parse(idElemento)))
                    {
                        //el elemento existe
                        retoorno = rolesInst.FirstOrDefault(p => p.Id == int.Parse(idElemento));
                        retoorno.Nombre = nombreElemento;
                        retoorno.Descripcion = descripcionElemento;
                        //update al registro

                        VCFramework.NegocioMySql.RolInstitucion.Modificar(retoorno);

                    }
                    else
                    {
                        //no existe el elemento
                        VCFramework.Entidad.Rol rol = roles.FirstOrDefault(p => p.Id == int.Parse(idOriginalElemento));
                        if (rol != null && rol.Id > 0)
                        {
                            retoorno.Descripcion = descripcionElemento;
                            retoorno.Eliminado = 0;
                            retoorno.IdOriginal = rol.Id;
                            retoorno.InstId = int.Parse(instIdElemento);
                            retoorno.Nombre = rol.Nombre;
                            //insert del registro
                            int idNuevo = VCFramework.NegocioMySql.RolInstitucion.Insertar(retoorno);
                            retoorno.Id = idNuevo;

                        }
                    }
                }
                else
                {
                    //no existe el elemento
                    VCFramework.Entidad.Rol rol = roles.FirstOrDefault(p => p.Id == int.Parse(idOriginalElemento));
                    if (rol != null && rol.Id > 0)
                    {
                        retoorno.Descripcion = descripcionElemento;
                        retoorno.Eliminado = 0;
                        retoorno.IdOriginal = rol.Id;
                        retoorno.InstId = int.Parse(instIdElemento);
                        retoorno.Nombre = rol.Nombre;
                        //insert del registro
                        int idNuevo = VCFramework.NegocioMySql.RolInstitucion.Insertar(retoorno);
                        retoorno.Id = idNuevo;

                    }
                }
                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(retoorno);
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