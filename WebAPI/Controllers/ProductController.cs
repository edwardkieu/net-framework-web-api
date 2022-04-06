using Service.Interfaces;
using Service.ViewModels;
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
        [Route()]
        public async Task<IHttpActionResult> GetAsync()
        {
            var result = await _productService.GetAllAsync();
            return ApiResponeSuccess(result);
        }

        [HttpPost]
        [Route()]
        public async Task<IHttpActionResult> PostAsync([FromBody] ProductViewModel vm)
        {
            var result = await _productService.AddAsync(vm);
            return ApiResponeSuccess(result);
        }
    }
}