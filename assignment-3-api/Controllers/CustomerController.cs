using assignment_3_api.Contexts;
using assignment_3_api.Models;
using assignment_3_api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assignment_3_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly DataContext _context;

        public CustomerController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CustomerRequest req)
        {
            _context.Add(new CustomerEntity
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                Email = req.Email
            });
            await _context.SaveChangesAsync();
            return new OkResult();
        }
        [HttpGet]
        public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            try
            {
                return await _context.Customers.ToListAsync();
            }
            catch { return null; }
        }

        //public string FullName(CustomerEntity customer)
        //{
        //    try
        //    {
        //        return customer.FirstName + " " + customer.LastName;
        //    }
        //    catch { return null; }
        //}
    }
}
