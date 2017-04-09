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
    public class AsistenteController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {
            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string nombreInstitucion = data.NombreInstitucion;
            
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            

            try
            {
                VCFramework.Entidad.Institucion institucion = new VCFramework.Entidad.Institucion();

                List<VCFramework.Entidad.Institucion> instituciones =  VCFramework.NegocioMySQL.Institucion.ListarInstitucionesSinCache();

                if (instituciones != null && instituciones.Count > 0)
                    institucion = instituciones.Find(p => p.Nombre.ToUpper() == nombreInstitucion.ToUpper());


                if (institucion != null && institucion.Id > 0)
                {
                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(institucion);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);
                }
                else
                {
                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject("Status: OK");
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


            string rut = data.Rut;
            string nombres = data.Nombres;
            string primerApellido = data.PrimerApellido;
            string segundoApellido = data.SegundoApellido;
            string telefono = data.Telefono;
            string correo = data.Correo;
            string idRegion = data.IdRegion;
            string idComuna = data.IdComuna;
            string direccion = data.Direccion;
            string password = data.Password;
            string nombreInstitucion = data.NombreInstitucion;

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            int idNuevo = 0;



            try
            {

                //creamos la institucion
                VCFramework.Entidad.Institucion institucion = new VCFramework.Entidad.Institucion();
                institucion.ComId = int.Parse(idComuna);
                institucion.CorreoElectronico = correo;
                institucion.Direccion = direccion;
                institucion.Eliminado = 0;
                institucion.Nombre = nombreInstitucion;
                institucion.RegId = int.Parse(idRegion);
                institucion.Telefono = telefono;
                //guardamos la institucion
                int idInstitucion = VCFramework.NegocioMySQL.Institucion.Insertar(institucion);
                if (idInstitucion > 0)
                {
                    //ahora creamos al usuario administrador
                    VCFramework.Entidad.AutentificacionUsuario aus = new VCFramework.Entidad.AutentificacionUsuario();
                    aus.CorreoElectronico = correo;
                    aus.EsVigente = 1;
                    aus.InstId = idInstitucion;
                    aus.NombreUsuario = rut;
                    aus.Password = password;
                    aus.RolId = 2; //administrador  
                    int idAus = VCFramework.NegocioMySQL.AutentificacionUsuario.InsertarAus(aus);
                    if (idAus > 0)
                    {
                        //ahora insertamos la persona
                        password = VCFramework.NegocioMySQL.Utiles.Encriptar(password);
                        VCFramework.Entidad.Persona per = new VCFramework.Entidad.Persona();
                        per.ApellidoMaterno = segundoApellido;
                        per.ApellidoPaterno = primerApellido;
                        per.ComId = int.Parse(idComuna);
                        per.DireccionCompleta = direccion;
                        per.InstId = idInstitucion;
                        per.Nombres = nombres;
                        per.PaisId = 1;
                        per.RegId = int.Parse(idRegion);
                        per.Rut = rut;
                        per.Telefonos = telefono;
                        per.UsuId = idAus;
                        //insertamos la persona
                        int idPer = VCFramework.NegocioMySQL.Persona.ModificarUsuario(per);
                        if (idPer > 0)
                        {

                            httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                            String JSON = JsonConvert.SerializeObject(per);
                            httpResponse.Content = new StringContent(JSON);
                            httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);
                        }
                        else
                        {

                            httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                            String JSON = JsonConvert.SerializeObject(null);
                            httpResponse.Content = new StringContent(JSON);
                            httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);
                        }

                    }
                    else
                    {
                        httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                        String JSON = JsonConvert.SerializeObject(null);
                        httpResponse.Content = new StringContent(JSON);
                        httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);
                    }
                }
                else
                {
                    httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                    String JSON = JsonConvert.SerializeObject(null);
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