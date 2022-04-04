using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace WebAPI.Filters
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override async Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            // Access Exception using context.Exception;
            //const string errorMessage = "An unexpected error occured";
            //var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
            //    new
            //    {
            //        Message = errorMessage
            //    });
            //response.Headers.Add("X-Error", errorMessage);
            //context.Result = new ResponseMessageResult(response);

            var exceptionMessage = await Task.FromResult(context.Exception.InnerException?.Message ?? context.Exception.Message);

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
            context.Result = new ResponseMessageResult(responseMessage);
        }
    }
}