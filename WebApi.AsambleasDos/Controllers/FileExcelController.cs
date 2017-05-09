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
using System.Data.OleDb;

namespace WebApi.AsambleasDos.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FileExcelController : ApiController
    {
        const string UploadDirectory = "~/Excel/";
        const string UploadDirectoryImg = "~/img/";
        
        [HttpPost]
        public HttpResponseMessage CreateContestEntry()
        {
            VCFramework.Entidad.DocumentosUsuario entidad = new VCFramework.Entidad.DocumentosUsuario();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string filesavepath="";
            try
            {
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedExcel"];
                string usuId = HttpContext.Current.Request.Form["UsuId"];
                string instId = HttpContext.Current.Request.Form["InstId"];

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
                    

                    filesavepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Excel"), httpPostedFile.FileName);

                    httpPostedFile.SaveAs(filesavepath);
                }

                StringBuilder retorno = new StringBuilder();
                retorno.Append("OK");


                //ahora procesamos el archivo
                #region proceso del archivo
                string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filesavepath + ";Extended Properties=Excel 12.0;Persist Security Info=False";
                
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);

                OleDbCommand cmd = new OleDbCommand("Select * from [Carga Masiva$]", excelConnection);
                excelConnection.Open();
                OleDbDataReader dReader;
                dReader = cmd.ExecuteReader();
                while (dReader.Read())
                {
                    //vamos a validar que los campos no esten vacios
                    if (!dReader.IsDBNull(0) && !dReader.IsDBNull(1) && !dReader.IsDBNull(2)  && !dReader.IsDBNull(4)
                        && !dReader.IsDBNull(6))
                    {
                        string rut = dReader.GetString(0);
                        string nombres = dReader.GetString(1);
                        string apPterno = dReader.GetString(2);
                        string apMaterno = dReader.IsDBNull(3) ? "" : dReader.GetString(3);
                        string nombreUsuario = dReader.GetString(4);
                        string telefono = dReader.IsDBNull(5) ? "" : dReader.GetString(5);
                        string correo = dReader.GetString(6);
                        string curso = dReader.IsDBNull(7) ? "" : dReader.GetString(7);

                        //ahora se deve validar



                    }
                }

                
                excelConnection.Close();
                



                #endregion

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(retorno);
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