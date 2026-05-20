using Microsoft.EntityFrameworkCore;

namespace BlazingPizza;

public class PizzaStoreContext : DbContext
{
  public PizzaStoreContext(DbContextOptions options) : base(options)
  {
  }

  public DbSet<PizzaSpecial> Specials { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<Pizza> Pizzas { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Pizza>()
        .Ignore(p => p.Toppings);
  }
}


