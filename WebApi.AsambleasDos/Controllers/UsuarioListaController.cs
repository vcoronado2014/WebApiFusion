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
    public class UsuarioListaController : ApiController
    {
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (data.LtrId == null)
                throw new ArgumentNullException("LtrId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string ltrId = data.LtrId;
                int ltrIdBuscar = int.Parse(ltrId);
                VCFramework.Entidad.UsuarioListaTricel devolver = new UsuarioListaTricel();

                List<VCFramework.Entidad.UsuarioLista> usuarios = VCFramework.NegocioMySQL.UsuarioLista.Obtener(ltrIdBuscar);

                if (usuarios != null && usuarios.Count > 0)
                {
                    foreach (VCFramework.Entidad.UsuarioLista usu in usuarios)
                    {

                        if (usu.Rol == "Presidente")
                            devolver.UsuIdPresidente = usu.UsuId;
                        if (usu.Rol == "VicePresidente")
                            devolver.UsuIdVicepresidente = usu.UsuId;
                        if (usu.Rol == "Tesorero")
                            devolver.UsuIdTesorero = usu.UsuId;
                        if (usu.Rol == "Secretario")
                            devolver.UsuIdSecretario = usu.UsuId;
                        if (usu.Rol == "OtroUno")
                            devolver.UsuIdOtroUno = usu.UsuId;
                        if (usu.Rol == "OtroDos")
                            devolver.UsuIdOtroDos = usu.UsuId;
                        if (usu.Rol == "OtroTres")
                            devolver.UsuIdOtroTres = usu.UsuId;
                        if (usu.Rol == "OtroCuatro")
                            devolver.UsuIdOtroCuatro = usu.UsuId;
                        if (usu.Rol == "OtroCinco")
                            devolver.UsuIdOtroCinco = usu.UsuId;

                        
                    }
                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(devolver);
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