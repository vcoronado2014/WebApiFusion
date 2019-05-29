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
using System.Net.Mail;

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PerfilController : ApiController
    {
        const string UploadDirectory = "~/Repositorio/";
        const string UploadDirectoryImg = "~/apps/img/";
        [HttpPost]
        public HttpResponseMessage UploadFile()
        {
            //if (HttpContext.Current.Request.Files.AllKeys.Any())
            //{
            // Get the uploaded image from the Files collection
            var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
            string input = HttpContext.Current.Request.Form["perfil"];

            string[] inputs = input.Split(',');
            //string Input = JsonConvert.SerializeObject(ppp);

            dynamic data = JObject.Parse(input);

            string id = data.Id;
            if (id == null)
                id = "0";
            int idBuscar = int.Parse(id);

            string esCpasStr = "false";
            bool esCpas = false;
            if (data.EsCpas != null)
            {
                esCpasStr = data.EsCpas;
                esCpas = Convert.ToBoolean(esCpasStr);
            }

            //validaciones antes de ejecutar la llamada.
            VCFramework.Entidad.MiPerfil aus = VCFramework.NegocioMySql.PrefilUsuario.BuscarPorId(idBuscar);
            VCFramework.Entidad.MiPerfil entidad = new VCFramework.Entidad.MiPerfil();
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string iniciales = data.Iniciales;
                string apodo = data.Apodo;
                string ausId = data.AusId;
                string nombreArchivo = data.Foto;
                string archivoGuardar = "";
                if (httpPostedFile != null && httpPostedFile.FileName != null)
                {
                    archivoGuardar = httpPostedFile.FileName;
                }
                else
                {
                    archivoGuardar = "#";
                }
                //quitar //
                string[] pedazos = nombreArchivo.Split('\\');
                string nom = pedazos[pedazos.Length - 1].ToString();

                if (aus == null)
                    aus = new VCFramework.Entidad.MiPerfil();

                if (aus != null)
                {

                    int nuevoId = 0;

                    entidad.Apodo = apodo;
                    entidad.Iniciales = iniciales;
                    entidad.AusId = int.Parse(ausId);


                    if (aus.Id == 0)
                    {
                        nuevoId = VCFramework.NegocioMySql.PrefilUsuario.Insertar(entidad);
                    }
                    else
                    {
                        nuevoId = aus.Id;
                        entidad.Id = nuevoId;
                        VCFramework.NegocioMySql.PrefilUsuario.Actualizar(entidad);
                    }


                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(entidad);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);


                }
            }
            catch (Exception ex)
            {
                httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                throw ex;
            }


            if (httpPostedFile != null)
            {

                // Validate the uploaded image(optional)

                // Get the complete file path
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/apps/Repositorio"), httpPostedFile.FileName);

                // Save the uploaded file to "UploadedFiles" folder
                httpPostedFile.SaveAs(fileSavePath);
            }

            return httpResponse;
        }
    }
}