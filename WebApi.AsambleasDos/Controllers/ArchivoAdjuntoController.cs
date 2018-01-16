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
    public class ArchivoAdjuntoController : ApiController
    {
        const string UploadDirectory = "~/Repositorio/";
        const string UploadDirectoryImg = "~/img/";

        [HttpPost]
        public HttpResponseMessage UploadFile()
        {
            //if (HttpContext.Current.Request.Files.AllKeys.Any())
            //{
            // Get the uploaded image from the Files collection
            var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
            string idElemento = HttpContext.Current.Request.Form["idElemento"];
            string instId = HttpContext.Current.Request.Form["instId"];
            string tipoPadre = HttpContext.Current.Request.Form["tipoPadre"];
            string nombreCarpeta = HttpContext.Current.Request.Form["nombreCarpeta"];
            string id = HttpContext.Current.Request.Form["id"];

            int idBuscar = int.Parse(id);
            int nuevoId = 0;
            //vamos a anteponer un descriptivo para el archivo
            string anteArchivo = idElemento + "_" + instId + "_";
            //si el id = 0 es un elemento nuevo, por lo tanto hay que insertarlo
            //se supone que el registro ya fue insertado (el del idelemento)
            bool esNuevo = false;
            bool guardarArchivo = false;
            if (idBuscar == 0)
                esNuevo = true;

            VCFramework.Entidad.ArchivoAdjunto entidad = new VCFramework.Entidad.ArchivoAdjunto();

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string archivoGuardar = anteArchivo;
                if (httpPostedFile != null && httpPostedFile.FileName != null)
                {
                    archivoGuardar = httpPostedFile.FileName;
                }

                if (esNuevo == false)
                {
                    entidad = VCFramework.NegocioMySql.ArchivoAdjunto.BuscarPorId(idBuscar);
                    if (entidad != null && entidad.Id > 0)
                    {
                        guardarArchivo = true;
                        entidad.NombreArchivo = archivoGuardar;
                        nuevoId = entidad.Id;
                        VCFramework.NegocioMySql.ArchivoAdjunto.Actualizar(entidad);

                    }
                }
                else
                {
                    guardarArchivo = true;
                    entidad.ElementoId = int.Parse(idElemento);
                    entidad.InstId = int.Parse(instId);
                    entidad.NombreArchivo = archivoGuardar;
                    entidad.NombreCarpeta = nombreCarpeta;
                    entidad.TipoPadre = int.Parse(tipoPadre);
                    nuevoId = VCFramework.NegocioMySql.ArchivoAdjunto.Insertar(entidad);
                    entidad.Id = nuevoId;
                }
                //ya ahora guardamos el archivo
                if (httpPostedFile != null && guardarArchivo)
                {

                    // Validate the uploaded image(optional)

                    // Get the complete file path
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/" + nombreCarpeta), archivoGuardar);
                    httpPostedFile.SaveAs(fileSavePath);
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
    }
}