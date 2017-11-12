using System.Web.Http;

namespace Banking.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "CustomApi",
               routeTemplate: "api/{controller}/{accountFrom}/{accountTo}/{amount}",
               defaults: new
               {
                   Controller = "Operation",
                   Action = "Perform",
                   accountFrom = RouteParameter.Optional,
                   accountTo = RouteParameter.Optional,
                   mount = RouteParameter.Optional
               }
           );
        }
    }
}
