using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace SignalR_Chat
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //RouteTable.Routes.MapHubs(new HubConfiguration()
            //{
            //    EnableCrossDomain = true
            //});
            //RouteTable.Routes.MapHubs();
        }
        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpResponse response = app.Response;

            if (response.StatusCode != 200)
            {
                //response.SuppressContent = true;
                //response.ClearContent();
                string host = HttpContext.Current.Request.Headers["Origin"];
                if (host != null && HttpContext.Current.Response.Headers["Access-Control-Allow-Origin"] == null)
                    HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", host);

                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
                HttpContext.Current.Response.End();
            }

        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            string host = HttpContext.Current.Request.Headers["Origin"];
            if (host != null)
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", host);
            }

            HttpApplication app = (HttpApplication)sender;
            if (app.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
                HttpContext.Current.Response.End();
                return;
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}