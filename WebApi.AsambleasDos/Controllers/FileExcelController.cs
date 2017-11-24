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
            string nombreArchivoGeneral = "";
            //el archivo se debe guardar con un nombre único

            try
            {
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedExcel"];
                string usuId = HttpContext.Current.Request.Form["UsuId"];
                string instId = HttpContext.Current.Request.Form["InstId"];
                //variables de los archivos
                string resultExtension = Path.GetExtension(httpPostedFile.FileName);
                string resultFileName = Path.ChangeExtension(httpPostedFile.FileName, resultExtension);
                string resultFileUrl = UploadDirectory + resultFileName;
                string fechaSubida = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                string urlExtension = "";

                if (httpPostedFile != null)
                {
                    //guardamos el registro
                    #region tratamiento del archivo
                    //string resultExtension = Path.GetExtension(httpPostedFile.FileName);
                    //string resultFileName = Path.ChangeExtension(httpPostedFile.FileName, resultExtension);
                    //string resultFileUrl = UploadDirectory + resultFileName;
                    //string fechaSubida = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                    //string urlExtension = "";
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
                    string nuevoArchivo = "c_m_" + usuId.ToString() + "_" + instId.ToString() + "_" + fechaStr + horaStr + resultExtension;
                    nombreArchivoGeneral = nuevoArchivo;



                    //filesavepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Excel"), httpPostedFile.FileName);
                    filesavepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Excel"), nuevoArchivo);

                    httpPostedFile.SaveAs(filesavepath);
                    
                }

                //ahora procesamos el archivo
                #region proceso del archivo

                //filesavepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Excel"), httpPostedFile.FileName);
                //filesavepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Excel"), nuevoArchivo);
                //esta variable permitira guardar los elementos del log ***************************************
                List<LogCarga> logCargaLista = new List<LogCarga>();
                //*********************************************************************************************
                StringBuilder sbErrores = new StringBuilder();
                StringBuilder sbCorrecto = new StringBuilder();
                List<string> listaCorreos = new List<string>();

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
                        int idRolGuardar = 0;
                        bool esCorrecto = true;
                        string rut = dReader.GetString(0);
                        string nombres = dReader.GetString(1);
                        string apPterno = dReader.GetString(2);
                        string apMaterno = dReader.IsDBNull(3) ? "" : dReader.GetString(3);
                        string nombreUsuario = dReader.GetString(4);
                       // string telefono = dReader.IsDBNull(5) ? "" : dReader.GetString(5);
                        string correo = dReader.GetString(5);
                        string nombreRol = dReader.IsDBNull(6) ? "" : dReader.GetString(6);
                        //nuevos campos ***********************************************************
                        //telefono optativo
                        string telefono = dReader.IsDBNull(7) ? "" : dReader.GetString(7);
                        //direccion
                        string direccion = dReader.IsDBNull(8) ? "" : dReader.GetString(8);
                        //**************************************************************************
                        //HAY QUE BUSCAR EL ROL POR NOMBRE
                        List<VCFramework.Entidad.RolInstitucion> roles = VCFramework.NegocioMySql.RolInstitucion.ObtenerRolesPorInstId(int.Parse(HttpContext.Current.Request.Form["InstId"]));
                        if (roles != null && roles.Count > 0 )
                        {
                            if (roles.Exists(p=>p.Nombre.ToUpper().Replace(" ", "") == nombreRol.ToUpper().Replace(" ", "")))
                            {
                                idRolGuardar = roles.FirstOrDefault(p => p.Nombre.ToUpper().Replace(" ", "") == nombreRol.ToUpper().Replace(" ", "")).Id;
                            }
                            else
                            {
                                sbErrores.AppendFormat("No existe el rol {0} es incorrecto en la fila {1}\r\n", nombreRol, numeroFila.ToString());
                                esCorrecto = false;
                                filasError++;
                                //entidad log
                                LogCarga ent = new LogCarga();
                                ent.Detalle = String.Format("No existe el rol {0} es incorrecto en la fila {1}\r\n", nombreRol, numeroFila.ToString());
                                ent.NombreUsuario = nombreUsuario;
                                ent.NumeroFila = numeroFila;
                                ent.TipoMensaje = 0;
                                logCargaLista.Add(ent);

                            }
                        }
                        else
                        {
                            sbErrores.AppendFormat("No existe el rol {0} es incorrecto en la fila {1}\r\n", nombreRol, numeroFila.ToString());
                            esCorrecto = false;
                            filasError++;
                            //entidad log
                            LogCarga ent = new LogCarga();
                            ent.Detalle = String.Format("No existe el rol {0} es incorrecto en la fila {1}\r\n", nombreRol, numeroFila.ToString());
                            ent.NombreUsuario = nombreUsuario;
                            ent.NumeroFila = numeroFila;
                            ent.TipoMensaje = 0;
                            logCargaLista.Add(ent);
                        }

                        //ahora se deve validar
                        //valida rut
                        if (VCFramework.NegocioMySQL.Utiles.validarRut(rut) == false)
                        {
                            sbErrores.AppendFormat("El rut {0} es incorrecto en la fila {1}\r\n", rut, numeroFila.ToString());
                            esCorrecto = false;
                            filasError++;
                            //break;
                            //entidad log
                            LogCarga ent = new LogCarga();
                            ent.Detalle = String.Format("El rut {0} es incorrecto en la fila {1}\r\n", rut, numeroFila.ToString());
                            ent.NombreUsuario = nombreUsuario;
                            ent.NumeroFila = numeroFila;
                            ent.TipoMensaje = 0;
                            logCargaLista.Add(ent);
                        }
                        //valida si username ya existe
                        VCFramework.Entidad.AutentificacionUsuario aus = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(nombreUsuario);
                        if (aus != null && aus.Id > 0)
                        {
                            sbErrores.AppendFormat("El nombre de usuario {0} ya existe en la fila {1}\r\n", nombreUsuario, numeroFila.ToString());
                            esCorrecto = false;
                            filasError++;
                            // break;
                            //entidad log
                            LogCarga ent = new LogCarga();
                            ent.Detalle = String.Format("El nombre de usuario {0} ya existe en la fila {1}\r\n", nombreUsuario, numeroFila.ToString());
                            ent.NombreUsuario = nombreUsuario;
                            ent.NumeroFila = numeroFila;
                            ent.TipoMensaje = 0;
                            logCargaLista.Add(ent);
                        }
                        //email
                        if (VCFramework.NegocioMySQL.Utiles.ValidaEmail(correo) == false)
                        {
                            sbErrores.AppendFormat("El correo electrónico {0} es incorrecto en la fila {1}\r\n", correo, numeroFila.ToString());
                            esCorrecto = false;
                            filasError++;
                            //break;
                            //entidad log
                            LogCarga ent = new LogCarga();
                            ent.Detalle = String.Format("El correo electrónico {0} es incorrecto en la fila {1}\r\n", correo, numeroFila.ToString());
                            ent.NombreUsuario = nombreUsuario;
                            ent.NumeroFila = numeroFila;
                            ent.TipoMensaje = 0;
                            logCargaLista.Add(ent);
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
                            ausGuardar.RolId = idRolGuardar;
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
                                persona.DireccionCompleta = direccion;
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
                                    listaCorreos.Add(correo + "," + nombreUsuario + "," + rut);
                                    //entidad log
                                    LogCarga ent = new LogCarga();
                                    ent.Detalle = String.Format("correcto {0} en la fila {1}\r\n", nombreUsuario, numeroFila.ToString());
                                    ent.NombreUsuario = nombreUsuario;
                                    ent.NumeroFila = numeroFila;
                                    ent.TipoMensaje = 1;
                                    logCargaLista.Add(ent);
                                }
                                else
                                {
                                    sbErrores.AppendFormat("error  guardar persona en la fila {0}\r\n", numeroFila.ToString());
                                    filasError++;
                                    //entidad log
                                    LogCarga ent = new LogCarga();
                                    ent.Detalle = String.Format("error  guardar persona en la fila {0}\r\n", numeroFila.ToString());
                                    ent.NombreUsuario = nombreUsuario;
                                    ent.NumeroFila = numeroFila;
                                    ent.TipoMensaje = 0;
                                    logCargaLista.Add(ent);
                                }
                            }
                            else
                            {
                                sbErrores.AppendFormat("error  guardar aus en la fila {0}\r\n", numeroFila.ToString());
                                filasError++;
                                //entidad log
                                LogCarga ent = new LogCarga();
                                ent.Detalle = String.Format("error  guardar aus en la fila {0}\r\n", numeroFila.ToString());
                                ent.NombreUsuario = nombreUsuario;
                                ent.NumeroFila = numeroFila;
                                ent.TipoMensaje = 0;
                                logCargaLista.Add(ent);
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

                //ahora vamos a procesar los correos electrónicos para enviarlos a todos los participantes.
                /*
                 *                                     VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(int.Parse(instId));
                                    VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                                    MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, false);
                                    //cr.Enviar(mnsj);
                                    var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                 * 
                 * */


                #endregion
                //ya ahora procesamos el encabezado de carga
                EncabezadoCarga encCarga = new EncabezadoCarga();
                encCarga.Fecha = DateTime.Now;
                encCarga.FilasCorrectas = correctas;
                encCarga.FilasError = filasError;
                encCarga.InstId = int.Parse(HttpContext.Current.Request.Form["InstId"]);
                encCarga.NombreArchivo = nombreArchivoGeneral;
                encCarga.Resumen = retorno.Mensaje;
                int idEncabezado = VCFramework.NegocioMySql.EncabezadoCarga.Insertar(encCarga);
                if (logCargaLista != null && logCargaLista.Count > 0)
                {
                    foreach(LogCarga log in logCargaLista)
                    {
                        log.EccId = idEncabezado;
                        VCFramework.NegocioMySql.LogCarga.Insertar(log);
                    }
                }


                if (httpPostedFile != null)
                {
                    if (filasError > 0)
                    {

                        File.Delete(filesavepath);
                    }
                }

                //procesamiento delos correos
                if (listaCorreos != null && listaCorreos.Count > 0)
                {
                    foreach(string s in listaCorreos)
                    {
                        //separados por coma
                        string[] arr = s.Split(',');
                        if (arr.Length == 3)
                        {
                            string email = arr[0].ToString();
                            string nombreUsu = arr[1].ToString();
                            string rut = arr[2].ToString();
                            StringBuilder mensaje = new StringBuilder();
                            mensaje.Append("Estimado usuario, usted ha sido creado en la PLataforma de Asambleas correctamente, para que pueda ingresar al sistema debe ");
                            mensaje.Append("entrar a la página web http://www.asambleas.cl y presionar en el menu Ingresar al Sistema, acto seguido en el nombre de usuario ");
                            mensaje.AppendFormat("debe ingresar {0} y en Password {1},", nombreUsu, rut);
                            mensaje.Append(" si usted no ha solicitado este acceso comúniquese con el administrador mediante el formulario de contacto de la misma página.");
                            mensaje.Append("\r\n");
                            mensaje.Append("\r\n");
                            mensaje.Append("\r\n");
                            mensaje.Append("*** mensaje enviado a través de la plataforma automática de asambleas.cl ***");
                            VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                            MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensaje(email, mensaje.ToString());
                            //cr.Enviar(mnsj);
                            var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));

                        }
                    }
                }

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