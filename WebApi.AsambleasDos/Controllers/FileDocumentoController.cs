﻿using System;
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
    public class FileDocumentoController : ApiController
    {
        const string UploadDirectory = "~/Repositorio/";
        const string UploadDirectoryImg = "~/apps/img/";
        [HttpPost]
        public HttpResponseMessage UploadFile(dynamic DynamicClass)
        {
            string input = "";
            dynamic data = null;

            if (DynamicClass != null)
            {
                input = JsonConvert.SerializeObject(DynamicClass);
            }
            else
            {

                input = HttpContext.Current.Request.Form["documento"];
            }


            data = JObject.Parse(input);

            var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
            string instId = data.InstId;
            if (instId == null)
                instId = "0";
            int idBuscar = int.Parse(instId);

            string usuId = data.UsuId;
            if (usuId == null)
                usuId = "0";

            string esCpasStr = "false";
            bool esCpas = false;
            if (data.EsCpas != null)
            {
                esCpasStr = data.EsCpas;
                esCpas = Convert.ToBoolean(esCpasStr);
            }


            //validaciones antes de ejecutar la llamada.

            VCFramework.Entidad.DocumentosUsuario entidad = new VCFramework.Entidad.DocumentosUsuario();
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
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
                    entidad.Extension = resultExtension;
                    entidad.FechaSubida = fechaSubida;
                    entidad.Id = 0;
                    entidad.InstId = int.Parse(instId);
                    entidad.Modificado = false;
                    entidad.NombreArchivo = httpPostedFile.FileName;
                    entidad.Nuevo = true;
                    entidad.Tamano = int.Parse(sizeInKilobytes.ToString());
                    entidad.UsuId = int.Parse(usuId);
                    entidad.Url = "";
                    VCFramework.NegocioMySQL.DocumentosUsuario.Insertar(entidad);

                    List<UsuariosCorreos> correos = UsuariosCorreos.ListaUsuariosCorreosPorInstId(entidad.InstId);
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
                        VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorId(entidad.InstId);
                        VCFramework.NegocioMySQL.ServidorCorreo cr = new VCFramework.NegocioMySQL.ServidorCorreo();

                        //MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeCrearProyecto(institucion.Nombre, tricel.Nombre, listaCorreos, false);
                        MailMessage mnsj = VCFramework.NegocioMySQL.Utiles.ConstruyeMensajeDocumento(institucion.Id, institucion.Nombre, entidad.NombreArchivo, listaCorreos, true, false, false, esCpas);

                        //cr.Enviar(mnsj);
                        var task = System.Threading.Tasks.Task.Factory.StartNew(() => cr.Enviar(mnsj));
                    }

                    #endregion

                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/apps/Repositorio"), httpPostedFile.FileName);

                    httpPostedFile.SaveAs(fileSavePath);
                }


                //construimos el retorno
                #region retorno
                List<VCFramework.Entidad.DocumentosUsuario> documentos = VCFramework.NegocioMySQL.DocumentosUsuario.ObtenerDocumentosPorInstIdNuevo(idBuscar);
                VCFramework.EntidadFuncional.proposalss documentosE = new VCFramework.EntidadFuncional.proposalss();
                documentosE.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                if (documentos != null && documentos.Count > 0)
                {
                    documentos = documentos.OrderByDescending(p => p.Id).ToList();
                    //documentos = documentos.OrderByDescending(p => p.FechaSubida).ToList();
                    foreach (VCFramework.Entidad.DocumentosUsuario doc in documentos)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio entidadS = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        entidadS.Id = doc.Id;
                        entidadS.NombreCompleto = doc.NombreArchivo;
                        entidadS.NombreUsuario = VCFramework.NegocioMySQL.Utiles.RetornaFechaDocumento(doc.FechaSubida);
                        entidadS.OtroUno = doc.Tamano.ToString()+ " Kb";

                        string extension = Path.GetExtension(doc.NombreArchivo);
                        entidadS.OtroTres = extension;

                        //HttpContext.Current.Server.MapPath("~/Repositorio")
                        string urlll = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/apps/Repositorio/";
                        if (doc.NombreArchivo != null && doc.NombreArchivo != "")
                            entidadS.Url = urlll + doc.NombreArchivo;
                        else
                            entidadS.Url = "#";

                        //vista previa del archivo
                        string urlVista = "http://docs.google.com/viewer?url=" + entidadS.Url + " &embedded=true";
                        entidadS.OtroDos = urlVista;


                        entidadS.UrlEliminar = "EliminarDocumento.html?id=" + entidadS.Id.ToString() + "&tipo=documentousuario";

                        documentosE.proposals.Add(entidadS);
                    }
                }
                #endregion

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(documentosE);
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