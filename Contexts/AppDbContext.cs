using Microsoft.EntityFrameworkCore;
using ORMMiniProject.Models;
using ORMMiniProject.NewFolder;
using ORMMiniProject.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.Contexts
{
    public class AppDbContext:DbContext
    {

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        public DbSet<Payment> Payments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constans.connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
