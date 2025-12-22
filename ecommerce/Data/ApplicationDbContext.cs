using ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=db29873.public.databaseasp.net; Database=db29873; User Id=db29873; Password=mH?5E9t+eL%3; Encrypt=False; MultipleActiveResultSets=True; ");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Mobiles", Description="this is Moblie" },
                new Category { Id = 2, Name = "Tablets", Description = "this is Tablet" },
                new Category { Id = 3, Name = "Laptop", Description = "this is Laptop" }
                );
        }
    }
}
