using api.DataConfig;
using api.Dtos.Product;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;


namespace api.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly DataContext _context;

        public ProductController(DataContext context, ITokenService tokenService)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            try { 
                List<ProductDto> productDto = await _context.Products.Select(item => new ProductDto{
                    Id = item.Id,
                    Name = item.Name,
                    BrandId = item.BrandId,
                    Category = item.Category,
                    Price = item.Price
                }).ToListAsync();
                return Ok(productDto);
            }
            catch (Exception ex) {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet("GetProductById/{Id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid Id)
        {
            try {
                ProductDto? product = await _context.Products.Where(p => p.Id == Id)
                    .Select(p => new ProductDto { 
                        Id = p.Id,
                        Name = p.Name,
                        BrandId = p.BrandId,
                        Category = p.Category,
                        Price = p.Price
                    })
                    .FirstOrDefaultAsync();
                if (product == null)
                {
                    return NotFound(new { message = "Product not found" });
                }
                return Ok(product);
            }
            catch (Exception ex) {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ProductDto>> Create([FromBody] ProductInputDto input)
        {
            try {
                ProductModel product = new ProductModel { 
                    Name = input.Name,
                    BrandId = input.BrandId,
                    Category = input.Category,  
                    Price = input.Price,
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                ProductDto productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    BrandId = product.BrandId,
                    Category = product.Category,
                    Price = product.Price,
                };
                return CreatedAtAction(nameof(GetProductById), new { Id = productDto.Id }, productDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] ProductDto input)
        {
            if (Id != input.Id)
            {
                return BadRequest(new { message = "ID mismatch" });
            }
            try
            {
                ProductModel? existingProduct = await _context.Products.FindAsync(Id);
                if (existingProduct == null)
                {
                    return NotFound(new { message = "Product not found" });
                }
                existingProduct.Name = input.Name;


                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == Id))
                {
                    return NotFound(new { message = "Product not found" });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex) {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }


        [HttpDelete("DeletedBy/{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try {
                ProductModel? productModel = await _context.Products.FindAsync(Id);
                if (productModel == null)
                {
                    return NotFound(new { message = "Product not found" });
                } 
                _context.Products.Remove(productModel);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Product deleted successfully" });
            }
            catch (Exception ex) {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
