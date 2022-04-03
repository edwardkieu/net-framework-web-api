using Common.ViewModels;
using Service.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI.Infrastructure.Core;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/v1/products")]
    public class ProductController : ApiControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAsync()
        {
            var result = await _productService.GetAllAsync();
            return ApiResponeSuccess(result);
        }
    }
}