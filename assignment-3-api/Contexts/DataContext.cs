﻿using assignment_3_api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace assignment_3_api.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderRowEntity>().HasKey(or => new { or.OrderId, or.ProductId });
        }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderRowEntity> OrderRows { get; set; }
    }
}
