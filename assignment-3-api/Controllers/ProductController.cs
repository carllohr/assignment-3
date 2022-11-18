using assignment_3_api.Contexts;
using assignment_3_api.Models;
using assignment_3_api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assignment_3_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductRequest req)
        {
            _context.Add(new ProductEntity
            {
                Name = req.Name,
                Description = req.Description,
                Price = req.Price
            });
            await _context.SaveChangesAsync();
            return new OkResult();
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductEntity>>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
            return new OkResult();
        }
        [HttpGet("{id}")]
        public async Task<ProductEntity> GetAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, ProductRequest req)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if(productEntity != null)
            {
                productEntity.Name = req.Name;
                productEntity.Description = req.Description;
                productEntity.Price = req.Price;
                _context.Entry(productEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            return new NotFoundResult();
           
        }
        [HttpDelete]
        public async Task<ActionResult<ProductModel>> DeleteAsync(int id)
        {
           
            try
            {
                var productEntity = await _context.Products.FindAsync(id);
                if (productEntity != null)
                {
                    _context.Products.Remove(productEntity);
                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch { return new NotFoundResult(); }
        }
    }
}
