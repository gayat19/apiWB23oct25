using FirstAPI.Interfaces;
using FirstAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles ="admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            try
            {
                var response = await _productService.AddProduct(request);
                return CreatedAtAction(nameof(AddProduct), new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] int id)
        {
            try
            {
                if(_productService.ChangeStatus(id,true).Result == false)
                {
                    return NotFound($"Product with id {id} not found.");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpGet("{id}")]
        [Route("GetProductById")]
        [HttpGet]
        public async Task<ActionResult<ProductListResponse>> GetProductById(int id)
        {
            try
            {
                var products = await _productService.GetAllProducts();
                var product = products.FirstOrDefault(p => p.Id == id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("RecontinueProduct")]
        [HttpPatch]
        public async Task<ActionResult<ProductListResponse>> ChangeStatus(int id)
        {
            try
            {
                if (_productService.ChangeStatus(id, false).Result == false)
                {
                    return NotFound($"Product with id {id} not found.");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
