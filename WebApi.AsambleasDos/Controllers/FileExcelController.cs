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
            string filesavepath = "";
            VCFramework.Entidad.CargaMasiva retorno = new CargaMasiva();
            int filasProcesadas = 0;
            int filasError = 0;
            int numeroFila = 1;
            //el archivo se debe guardar con un nombre único

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
                    //cambiamos nombre archivo
                    string fechaStr = VCFramework.NegocioMySQL.Utiles.RetornaFechaEntera();
                    string horaStr = VCFramework.NegocioMySQL.Utiles.RetornaHoraEntera();
                    string nuevoArchivo = "c_m_" + usuId.ToString() + "_" + instId.ToString() + "_" + fechaStr + horaStr  + resultExtension;



                    //filesavepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Excel"), httpPostedFile.FileName);
                    filesavepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Excel"), nuevoArchivo);

                    httpPostedFile.SaveAs(filesavepath);
                }

                //ahora procesamos el archivo
                #region proceso del archivo

                StringBuilder sbErrores = new StringBuilder();
                StringBuilder sbCorrecto = new StringBuilder();

                string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filesavepath + ";Extended Properties=Excel 12.0;Persist Security Info=False";
                
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);

                OleDbCommand cmd = new OleDbCommand("Select * from [Carga Masiva$]", excelConnection);
                excelConnection.Open();
                OleDbDataReader dReader;
                dReader = cmd.ExecuteReader();
                while (dReader.Read())
                {
                    //vamos a validar que los campos no esten vacios
                    if (dReader.IsDBNull(0) == false && dReader.IsDBNull(1) == false && dReader.IsDBNull(2) == false && dReader.IsDBNull(4) == false
                        && dReader.IsDBNull(6) == false)
                    {
                        bool esCorrecto = true;
                        string rut = dReader.GetString(0);
                        string nombres = dReader.GetString(1);
                        string apPterno = dReader.GetString(2);
                        string apMaterno = dReader.IsDBNull(3) ? "" : dReader.GetString(3);
                        string nombreUsuario = dReader.GetString(4);
                        string telefono = dReader.IsDBNull(5) ? "" : dReader.GetString(5);
                        string correo = dReader.GetString(6);
                        string curso = dReader.IsDBNull(7) ? "" : dReader.GetString(7);

                        //ahora se deve validar
                        //valida rut
                        if (VCFramework.NegocioMySQL.Utiles.validarRut(rut) == false)
                        {
                            sbErrores.AppendFormat("El rut {0} es incorrecto en la fila {1}\r\n", rut, numeroFila.ToString());
                            esCorrecto = false;
                            filasError++;
                            //break;
                        }
                        //valida si username ya existe
                        VCFramework.Entidad.AutentificacionUsuario aus = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(nombreUsuario);
                        if (aus != null && aus.Id > 0)
                        {
                            sbErrores.AppendFormat("El nombre de usuario {0} ya existe en la fila {1}\r\n", nombreUsuario, numeroFila.ToString());
                            esCorrecto = false;
                            filasError++;
                           // break;
                        }
                        //email
                        if (VCFramework.NegocioMySQL.Utiles.ValidaEmail(correo) == false)
                        {
                            sbErrores.AppendFormat("El correo electrónico {0} es incorrecto en la fila {1}\r\n", correo, numeroFila.ToString());
                            esCorrecto = false;
                            filasError++;
                            //break;
                        }
                        //si es correcto se agrega a la lista para guardar
                        if (esCorrecto)
                        {
                            VCFramework.Entidad.AutentificacionUsuario ausGuardar = new VCFramework.Entidad.AutentificacionUsuario();
                            ausGuardar.CorreoElectronico = correo;
                            ausGuardar.EsVigente = 1;
                            ausGuardar.InstId = int.Parse(instId);
                            ausGuardar.NombreUsuario = nombreUsuario;
                            ausGuardar.Password = VCFramework.NegocioMySQL.Utiles.Encriptar(rut);
                            //por defecto apoderado
                            ausGuardar.RolId = 9;
                            //listaAusProcesar.Add(ausGuardar);
                            int idAu = VCFramework.NegocioMySQL.AutentificacionUsuario.InsertarAus(ausGuardar);
                            if (idAu > 0)
                            {
                                //njos traemos la institucion 
                                VCFramework.Entidad.Institucion inst =  VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(int.Parse(instId));
                                //ahora guardamos la persona
                                VCFramework.Entidad.Persona persona = new VCFramework.Entidad.Persona();
                                persona.ApellidoMaterno = apMaterno;
                                persona.ApellidoPaterno = apPterno;
                                persona.ComId = inst.ComId;
                                persona.DireccionCompleta = inst.Direccion;
                                persona.InstId = inst.Id;
                                persona.Nombres = nombres;
                                persona.PaisId = 1;
                                persona.RegId = inst.RegId;
                                persona.Rut = rut;
                                persona.Telefonos = telefono;
                                persona.UsuId = idAu;
                                int perId = VCFramework.NegocioMySQL.Persona.ModificarUsuario(persona);
                                if (perId > 0)
                                {
                                    //todo correcto
                                }
                                else
                                {
                                    sbErrores.AppendFormat("error  guardar persona en la fila {0}\r\n", numeroFila.ToString());
                                    filasError++;
                                }
                            }
                            else
                            {
                                sbErrores.AppendFormat("error  guardar aus en la fila {0}\r\n", numeroFila.ToString());
                                filasError++;
                            }


                        }

                        numeroFila++;
                        filasProcesadas++;


                    }
                }


                int correctas = filasProcesadas - filasError;
                sbCorrecto.AppendFormat("Se procesaron {0} filas, se encontraron {1} errores.", filasProcesadas.ToString(), filasError);
                if (sbErrores.Length > 0)
                {
                    retorno.Mensaje = sbCorrecto.ToString() + "\r\n" + sbErrores.ToString();

                }
                else
                    retorno.Mensaje = sbCorrecto.ToString();

                retorno.FilasError = filasError;
                retorno.FilasProcesadas = filasProcesadas;

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