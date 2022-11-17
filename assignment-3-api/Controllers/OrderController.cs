using assignment_3_api.Contexts;
using assignment_3_api.Models;
using assignment_3_api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace assignment_3_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(OrderRequest req)
        {
            var orderEntity = new OrderEntity
            {
                Created = req.OrderDate = DateTime.Now,
                CustomerId = req.CustomerId,
                TotalPrice = req.TotalPrice,


            };
            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();

            foreach(var product in req.Products)
            {
                var orderRow = new OrderRowEntity
                {
                    Quantity = product.Quantity,
                    ProductId = product.Id,
                    OrderId = orderEntity.Id,
                    Price = req.RowPrice




                };
                _context.OrderRows.Add(orderRow);
                await _context.SaveChangesAsync();

            }


            return new OkResult();
        }
    }
}
