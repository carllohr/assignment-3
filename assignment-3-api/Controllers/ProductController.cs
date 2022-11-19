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
        public async Task<IActionResult> CreateAsync(ProductRequest req) //creates new productentity
        {
            try
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
            catch { return new NotFoundResult(); }    
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductEntity>>> GetAllAsync() // gets all products from database to a list
        {
            try
            {
                return await _context.Products.ToListAsync();
                return new OkResult();
            }
            catch { return new NotFoundResult(); }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, ProductRequest req) //updates product information in database
        {
            try
            {
                var productEntity = await _context.Products.FindAsync(id);
                if (productEntity != null)
                {
                    productEntity.Name = req.Name;
                    productEntity.Description = req.Description;
                    productEntity.Price = req.Price;
                    _context.Entry(productEntity).State = EntityState.Modified;
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
        [HttpDelete]
        public async Task<ActionResult<ProductModel>> DeleteAsync(int id) //deletes a product in the database by productid as argument
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
