using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace Banking.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // config.Routes.MapHttpRoute(
            //    name: "PerformApi",
            //    routeTemplate: "api/{controller}/{accountFrom}/{accountTo}/{amount}",
            //    defaults: new
            //    {
            //        Controller = "Operation",
            //        Action = "Perform",
            //        accountFrom = RouteParameter.Optional,
            //        accountTo = RouteParameter.Optional,
            //        mount = RouteParameter.Optional
            //    }
            //);

            // config.Routes.MapHttpRoute(
            //    name: "TokenApi",
            //    routeTemplate: "api/{controller}/{username}/{password}",
            //    defaults: new
            //    {
            //        Controller = "Token",
            //        Action = "Get",
            //        username = RouteParameter.Optional,
            //        password = RouteParameter.Optional
            //    }
            //);

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
