﻿using assignment_3_api.Contexts;
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
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(int id, CustomerRequest req)
        {
            var customerEntity = await _context.Customers.FindAsync(id);
            if (customerEntity != null)
            {
                customerEntity.FirstName = req.FirstName;
                customerEntity.LastName = req.LastName;
                customerEntity.Email = req.Email;
                _context.Entry(customerEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            return new NotFoundResult();

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
    }
}
