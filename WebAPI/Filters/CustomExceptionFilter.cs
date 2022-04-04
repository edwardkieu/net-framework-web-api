using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace WebAPI.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exceptionMessage = actionExecutedContext.Exception.InnerException?.Message ?? actionExecutedContext.Exception.Message;

            //We can log this exception message to the file or database.
            var response = new
            {
                Succeeded = false,
                Message = $"An unhandled exception was thrown by service. {exceptionMessage}"
            };
            var responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent(JsonConvert.SerializeObject(response, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() })),
                ReasonPhrase = "Internal Server Error.Please Contact your Administrator."
            };
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            actionExecutedContext.Response = responseMessage;
        }
    }
}