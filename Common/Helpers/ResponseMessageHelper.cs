using Common.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Common.Helpers
{
    public static class ResponseMessageHelper
    {
        public static HttpResponseMessage CreateResponse<T>(HttpStatusCode statusCode = HttpStatusCode.OK, T data = default(T))
        {
            var response = new ResponseViewModel<T>(data);
            var responseMessage = new HttpResponseMessage()
            {
                StatusCode = statusCode,
                Content = new StringContent(JsonConvert.SerializeObject(response, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() })),
            };
            responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return responseMessage;
        }
    }
}