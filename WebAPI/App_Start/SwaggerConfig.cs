using Swashbuckle.Application;
using System.Web.Http;
using WebActivatorEx;
using WebAPI;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "WebAPI");
                })
                .EnableSwaggerUi();
        }
    }
}