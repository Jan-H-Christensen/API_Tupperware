using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplication1WebHook
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //var cors = new EnableCorsAttribute("www.example.com", "*", "*");
            //config.EnableCors(cors);
            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Here: 
            //config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            config.InitializeReceiveGenericJsonWebHooks();
        }
    }
}
