
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Products;
using Swashbuckle.AspNetCore.Annotations;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [SwaggerOperation("Returns a list of products")]
        [HttpGet, AllowAnonymous]
        public Task<ProductResponse.GetIndex> GetIndex([FromQuery] ProductRequest.GetIndex request)
        {
            return productService.GetIndexAsync(request);
        }

        [SwaggerOperation("Returns a product with the correspondig ProductId")]
        [HttpGet("{ProductId}"), AllowAnonymous]
        public Task<ProductResponse.GetDetail> GetDetail([FromRoute] ProductRequest.GetDetail request)
        {
            return productService.GetDetailAsync(request);
        }
    }
}
