using Service.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Infrastructure.Core;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/v1/leaves")]
    public class LeaveController : ApiControllerBase
    {
        private readonly ILeaveService _leaveService;

        public LeaveController(ILeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        [HttpPost]
        [Route("allocations/departments/{departmentId}")]
        public async Task<IHttpActionResult> AllocateForDepartmentAsync(int departmentId)
        {
            return ApiResponeSuccess(true);
        }
    }
}