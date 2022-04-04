using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using WebAPI.Filters;

namespace WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            // Add Custom validation filters  
            config.Filters.Add(new ValidateModelStateFilter());

            // exception for action scope
            config.Filters.Add(new CustomExceptionFilter());

            // global scope
            //config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // Configure Json Format
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver
            { 
                IgnoreSerializableAttribute = true
            };
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

            // Authentication Configure
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
