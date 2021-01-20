using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration; // for the multiplicity stuff most likely
using PizzaBox.Domain.Models;
using PizzaBox.Domain.Abstracts;
using System.Collections.Generic;

namespace PizzaBox.Storing
{
  public class PizzaBoxContext : DbContext
  {
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Crust> Crusts { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Topping> Toppings { get; set; }
    public DbSet<PizzaTopping> PizzaToppings { get; set; }

    public PizzaBoxContext(DbContextOptions<PizzaBoxContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Customer>().HasKey(cu => cu.EntityId);
      builder.Entity<Order>().HasKey(o => o.EntityId);
      builder.Entity<Store>().HasKey(s => s.EntityId);
      builder.Entity<APizzaModel>().HasKey(p => p.EntityId);
      builder.Entity<Crust>().HasKey(c => c.EntityId);
      builder.Entity<Size>().HasKey(sz => sz.EntityId);
      builder.Entity<PizzaTopping>().HasKey(pt => pt.EntityId);
      builder.Entity<Topping>().HasKey(t => t.EntityId);

      // builder.Entity<Order>().HasOne(o => o.Store).WithMany(s => s.Orders);
      // builder.Entity<Store>().HasMany(s => s.Orders).WithOne(o => o.Store);
      // builder.Entity<Pizza>().HasMany<Topping>(p => p.Ingredients).WithMany(t => t.Pizzas);

      SeedData(builder);
    }

    private void SeedData(ModelBuilder builder)
    {
      builder.Entity<Store>().HasData(
        new Store() { EntityId = 1, Name = "Texas" },
        new Store() { EntityId = 2, Name = "Maryland" },
        new Store() { EntityId = 3, Name = "Florida" }
      );
      builder.Entity<Crust>().HasData(new List<Crust>()
          {
              new Crust { EntityId = 1, Name = "Thin", Price = .99m },
              new Crust { EntityId = 2, Name = "Regular", Price = 1.99m },
              new Crust { EntityId = 3, Name = "Large", Price = 2.99m }
          }
      );
      builder.Entity<Size>().HasData(new List<Size>()
          {
              new Size { EntityId = 1, Name = "Small", Inches = 10, Price = .99m },
              new Size { EntityId = 2, Name = "Medium", Inches = 12, Price = 2.99m },
              new Size { EntityId = 3, Name = "Large", Inches = 14, Price = 4.99m }
          }
      );
      builder.Entity<Topping>().HasData(new List<Topping>()
          {
              new Topping { EntityId = 1, Name = "Cheese"},
              new Topping { EntityId = 2, Name = "Pepperoni"},
              new Topping { EntityId = 3, Name = "Sausage"},
              new Topping { EntityId = 4, Name = "Pineapple"},
              new Topping { EntityId = 5, Name = "Tomato Sauce"},
              new Topping { EntityId = 6, Name = "Shrimp"},
              new Topping { EntityId = 7, Name = "Crab"}
          }
      );
    }
  }
}
