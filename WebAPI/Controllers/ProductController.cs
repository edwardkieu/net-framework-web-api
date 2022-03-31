using Common.ViewModels;
using Service.Interfaces;
using Service.ViewModels;
using System.Collections;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Infrastructure.Core;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/v1/product")]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IHttpActionResult> GetAsync()
        {
            var result = await _productService.GetAllAsync();
            var response = new ResponseViewModel<object>(result);
            return Ok(response);
        }
    }
}