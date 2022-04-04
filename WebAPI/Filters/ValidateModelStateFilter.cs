using Common.Helpers;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                //actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
                //actionContext.Response = ResponseMessageHelper.CreateResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}