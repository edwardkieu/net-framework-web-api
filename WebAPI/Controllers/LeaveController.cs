using Common.Enums;
using Microsoft.AspNet.Identity;
using Service.Interfaces;
using Service.ViewModels;
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

        [HttpGet, Route("departments/{departmentId}")]
        public async Task<IHttpActionResult> CreateRequestLeaveAsync([FromUri] int departmentId)
        {
            var result = await _leaveService.GetByDepartmentIdAsync(departmentId);

            return ApiResponeSuccess(result);
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> GetByIdAsync([FromUri] int id)
        {
            var result = await _leaveService.GetByIdAsync(id);

            return ApiResponeSuccess(result);
        }

        [HttpPost, Route("allocations/departments/{departmentId}")]
        public async Task<IHttpActionResult> AllocateForDepartmentAsync([FromUri] int departmentId, [FromUri] int period)
        {
            var result = await _leaveService.AllLocateLeaveByDepartment(departmentId, period);

            return ApiResponeSuccess(result);
        }

        [HttpPost, Route("create")]
        public async Task<IHttpActionResult> CreateRequestLeaveAsync([FromBody] CreateRequestLeaveViewModel vm)
        {
            var result = await _leaveService.CreateRequestLeaveAsync(vm);

            return ApiResponeSuccess(result);
        }

        [HttpPut, Route("{id}/update-status")]
        public async Task<IHttpActionResult> ChangeStatusAsync([FromUri]int id, [FromUri]RequestLeaveStatus status)
        {
            var result = await _leaveService.ChangeStatusAsync(id, status);

            return ApiResponeSuccess(result);
        }
    }
}