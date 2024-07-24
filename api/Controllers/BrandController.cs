using api.DataConfig;
using api.Dtos;
using api.Dtos.Brand;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace api.Controllers
{
    public class BrandController : BaseApiController
    {
        private readonly DataContext _context;

        public BrandController(DataContext context, ITokenService tokenService)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAll()
        {
            try
            {
                List<BrandDto> brandDtos = await _context.Brands
                    .Select(item => new BrandDto
                    {
                        Id = item.Id,
                        Name = item.Name,
                    })
                    .ToListAsync();

                return Ok(brandDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet("GetBrandById/{Id}")]
        public async Task<ActionResult<BrandDto>> GetBrandById(Guid Id)
        {
            try
            {
                BrandDto? brand = await _context.Brands
                    .Where(b => b.Id == Id)
                    .Select(b => new BrandDto
                    {
                        Id = b.Id,
                        Name = b.Name,
                    })
                    .FirstOrDefaultAsync();

                if (brand == null)
                {
                    return NotFound(new { message = "Brand not found" });
                }

                return Ok(brand);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<BrandDto>> Create([FromBody] BrandInputDto input)
        {
            try
            {
                BrandModel brand = new BrandModel
                {
                    Name = input.Name,
                };

                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();

                BrandDto brandDto = new BrandDto
                {
                    Id = brand.Id,
                    Name = brand.Name,
                };

                return CreatedAtAction(nameof(GetBrandById), new { Id = brandDto.Id }, brandDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPut("Update/{Id}")]
        public async Task<IActionResult> Update(Guid Id, [FromBody] BrandDto input)
        {
            if (Id != input.Id)
            {
                return BadRequest(new { message = "ID mismatch" });
            }

            try
            {
                BrandModel? existingBrand = await _context.Brands.FindAsync(Id);

                if (existingBrand == null)
                {
                    return NotFound(new { message = "Brand not found" });
                }

                existingBrand.Name = input.Name;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Brands.Any(e => e.Id == Id))
                {
                    return NotFound(new { message = "Brand not found" });
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpDelete("DeletedBy/{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                BrandModel? brand = await _context.Brands.FindAsync(Id);

                if (brand == null)
                {
                    return NotFound(new { message = "Brand not found" });
                }

                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Brand deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
