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
    public class ArchivoProyectoController : ApiController
    {
        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage Get([FromUri]string ProyectoId)
        {

            //validaciones antes de ejecutar la llamada.
            if (ProyectoId == "0")
                throw new ArgumentNullException("ProyectoId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string proyectoId = ProyectoId;
                int proyectoIdBuscar = int.Parse(ProyectoId);

                VCFramework.EntidadFuncional.proposalss votaciones = new VCFramework.EntidadFuncional.proposalss();
                votaciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.ArchivosProyecto> archivos = VCFramework.NegocioMySQL.ArchivosProyecto.ObtenerArchivosPorProyectoId(proyectoIdBuscar, null);

                if (archivos != null && archivos.Count > 0)
                {
                    foreach (VCFramework.Entidad.ArchivosProyecto tri in archivos)
                    {
                        string urlll = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/RepositorioProyecto/";

                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = tri.Id;
                        us.NombreCompleto = tri.RutaArchivo;

                        us.Url = urlll + us.NombreCompleto;
                        us.UrlEliminar = "EliminarDocumento.html?id=" + us.Id.ToString() + "&tipo=archivoproyecto&proyectoId=" + tri.ProId.ToString();

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

        const string UploadDirectory = "~/RepositorioProyecto/";
        const string UploadDirectoryImg = "~/img/";
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage CreateContestEntry()
        {

            VCFramework.Entidad.ArchivosProyecto entidad = new VCFramework.Entidad.ArchivosProyecto();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                string proId = HttpContext.Current.Request.Form["ProId"];

                if (httpPostedFile != null)
                {
                    //guardamos el registro
                    #region tratamiento del archivo
                    string resultExtension = Path.GetExtension(httpPostedFile.FileName);
                    string resultFileName = Path.ChangeExtension(httpPostedFile.FileName, resultExtension);
                    string resultFileUrl = UploadDirectory + resultFileName;
                    string fechaSubida = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                    string urlExtension = "";
                    switch (resultExtension)
                    {
                        case ".jpg":
                        case ".jpeg":
                            urlExtension = UploadDirectoryImg + "jpeg.png";
                            break;
                        case ".gif":
                            urlExtension = UploadDirectoryImg + "gif.png";
                            break;
                        case ".png":
                            urlExtension = UploadDirectoryImg + "png.png";
                            break;
                        case ".doc":
                        case ".docx":
                            urlExtension = UploadDirectoryImg + "word.png";
                            break;
                        case ".xls":
                        case ".xlsx":
                            urlExtension = UploadDirectoryImg + "excel.png";
                            break;
                        case ".pdf":
                            urlExtension = UploadDirectoryImg + "pdf.png";
                            break;
                    }

                    string name = httpPostedFile.FileName;
                    long sizeInKilobytes = httpPostedFile.ContentLength / 1024;
                    string sizeText = sizeInKilobytes.ToString() + " KB";
                    #endregion

                    //guardamos el registro
                    #region guardado registro
                    entidad.Borrado = false;
                    entidad.Eliminado = 0;
                    entidad.Id = 0;
                    entidad.ProId = int.Parse(proId);
                    entidad.Modificado = false;
                    entidad.RutaArchivo = httpPostedFile.FileName;
                    entidad.Nuevo = true;
                    entidad.Url = "";
                    VCFramework.NegocioMySQL.ArchivosProyecto.Insertar(entidad);
                    #endregion

                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/RepositorioProyecto"), httpPostedFile.FileName);

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
                string idTricel = data.Id;
                int idTricelBuscar = int.Parse(idTricel);
                List<VCFramework.Entidad.ArchivosProyecto> inst = VCFramework.NegocioMySQL.ArchivosProyecto.ObtenerArchivoPorId(idTricelBuscar);

                if (inst != null && inst.Count == 1)
                {

                    VCFramework.NegocioMySQL.ArchivosProyecto.Eliminar(inst[0]);

                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(inst[0]);
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