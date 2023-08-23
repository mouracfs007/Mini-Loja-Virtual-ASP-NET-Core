using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Roles;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var productsDto = await _productService.GetProducts();
            if (productsDto == null)
            {
                return NotFound("Products not found");
            }
            return Ok(productsDto);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get(int id)
        {
            var productDto = await _productService.GetProductById(id);
            if (productDto == null)
            {
                return NotFound("Product not found");
            }
            return Ok(productDto);
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDto)
        {

            if (productDto == null)
            {
                return BadRequest("Data Invalid");
            }
            await _productService.AddProduct(productDto);

            return new CreatedAtRouteResult("GetProduct", new { id = productDto.Id },
                productDto);
        }

        [HttpPut]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductDTO>> Put([FromBody] ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Data Invalid");
            }

            await _productService.UpdateProduct(productDto);
            return Ok(productDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var productDto = await _productService.GetProductById(id);

            if (productDto == null)
            {
                return NotFound("Product not found");
            }

            await _productService.RemoveProduct(id);
            return Ok(productDto);
        }
    }
}
