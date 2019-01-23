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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace WebApi.AsambleasDos.Controllers
{
    public class ArchivoAdjuntoController : ApiController
    {
        const string UploadDirectory = "~/Repositorio/";
        const string UploadDirectoryImg = "~/img/";

        [HttpPost]
        public HttpResponseMessage UploadFile()
        {

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
                    entidad = VCFramework.NegocioMySql.ArchivoAdjunto.BuscarPorIdElemento(int.Parse(idElemento));
                    if (entidad != null && entidad.Id > 0)
                    {
                        //aca debemos determinar si el archivo se inserta o modifica
                        if (entidad.NombreArchivo == httpPostedFile.FileName)
                        {
                            guardarArchivo = true;
                            entidad.NombreArchivo = archivoGuardar;
                            nuevoId = entidad.Id;
                            VCFramework.NegocioMySql.ArchivoAdjunto.Actualizar(entidad);
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
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/" + nombreCarpeta), archivoGuardar);
                    //guardamos 
                    httpPostedFile.SaveAs(fileSavePath);
                    //antes de todo vamos a trabajar un rato con el archivo
                    Image imagenOriginal = VCFramework.NegocioMySQL.Utiles.NonLockingOpen(fileSavePath);
                    int ancho = imagenOriginal.Width;

                    //aca debemos procesar si la imagen excede los 1024 px
                    if (ancho > 1024)
                    {
                        //deberiamos redimensionar la imagen
                        string nameResize = ResizeImage(fileSavePath, fileSavePath, 1024, 768);


                    }

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
        public HttpResponseMessage Delete([FromUri]string Id)
        {

            //string Input = JsonConvert.SerializeObject(DynamicClass);

            //dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (Id == "")
                throw new ArgumentNullException("Id");


            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string id = Id;
                int idInt = int.Parse(id);
                VCFramework.Entidad.ArchivoAdjunto archivo = VCFramework.NegocioMySql.ArchivoAdjunto.BuscarPorId(idInt);

                if (archivo != null && archivo.Id > 0)
                {

                    VCFramework.NegocioMySql.ArchivoAdjunto.Eliminar(archivo);

                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(archivo);
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

        public static string ResizeImage(string strImgPath, string strImgOutputPath, int iWidth, int iHeight)
        {
            try
            {
                bool mismaImagen = strImgPath.Equals(strImgOutputPath);
                if (mismaImagen)
                {
                    strImgOutputPath = strImgPath + "___.jpg";
                }

                string[] extensiones = {
                                   ".jpg",
                                   ".png",
                                   ".bmp",
                                   ".gif",
                                   ".JPG",
                                   ".PNG",
                                   ".BMP",
                                   ".GIF"
                               };

                if (!extensiones.Contains(Path.GetExtension(strImgPath)))
                    throw new Exception("Extensión no soportada");

                //Lee el fichero en un stream
                Stream mystream = null;

                if (strImgPath.StartsWith("http"))
                {
                    HttpWebRequest wreq = (HttpWebRequest)WebRequest.Create(strImgPath);
                    HttpWebResponse wresp = (HttpWebResponse)wreq.GetResponse();
                    mystream = wresp.GetResponseStream();
                }
                else
                    mystream = File.OpenRead(strImgPath);

                // Cargo la imágen
                Bitmap imgToResize = new Bitmap(mystream);

                Size size = new Size(iWidth, iHeight);

                int sourceWidth = imgToResize.Width;
                int sourceHeight = imgToResize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)size.Width / (float)sourceWidth);
                nPercentH = ((float)size.Height / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(sourceWidth * nPercent);
                int destHeight = (int)(sourceHeight * nPercent);

                Bitmap b = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
                g.Dispose();

                // We will store the correct image codec in this object
                ImageCodecInfo ici = GetEncoderInfo("image/jpeg"); ;
                // This will specify the image quality to the encoder
                EncoderParameter epQuality = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 99L);
                // Store the quality parameter in the list of encoder parameters
                EncoderParameters eps = new EncoderParameters(1);
                eps.Param[0] = epQuality;
                b.Save(strImgOutputPath, ici, eps);

                imgToResize.Dispose();
                mystream.Close();
                mystream.Dispose();
                b.Dispose();
                g.Dispose();

                if (mismaImagen)
                {
                    File.Delete(strImgPath);
                    File.Move(strImgOutputPath, strImgPath);
                }

                return strImgPath;
            }
            catch
            {
                throw;
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}