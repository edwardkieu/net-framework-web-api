using System.Web.Http;

namespace WebAPI.Infrastructure.Core
{
    [Authorize]
    public class ApiControllerBase : ApiController
    {
        public ApiControllerBase()
        {
        }
    }
}