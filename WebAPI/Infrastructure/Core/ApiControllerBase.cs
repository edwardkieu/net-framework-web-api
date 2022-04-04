using Common.ViewModels;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Infrastructure.Core
{
    [Authorize]
    public class ApiControllerBase : ApiController
    {
        public ApiControllerBase()
        {
        }

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage requestMessage, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response;
            try
            {
                response = function.Invoke();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                // Can log ex here
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                // Can log dbEx here
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                // Can log ex here
                response = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return response;
        }

        protected IHttpActionResult ApiPagedResponeSuccess(object data, int totalCount = 0, int pageNumber = 0, int pageSize = 0)
        {
            return Ok(new PagedResponseViewModel<object>(data, totalCount, pageNumber, pageSize));
        }

        protected IHttpActionResult ApiResponeSuccess(object data)
        {
            return Ok(new ResponseViewModel<object>(data));
        }

        protected IHttpActionResult ApiResponeBadRequest(object data, string message = "")
        {
            return Ok(new ResponseViewModel<object>(data ?? null, message ?? "BadRequest"));
        }

        protected IHttpActionResult ApiResponeForbidden(string message = "Forbidden")
        {
            return Ok(new ResponseViewModel<object>(message));
        }
    }
}