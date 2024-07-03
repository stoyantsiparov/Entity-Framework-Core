using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data;

public class SalesContext : DbContext
{
    private const string ConnectionString = "Server=OMEN\\SQLEXPRESS;Database=SalesContext;Integrated Security=True;";

    public SalesContext()
    {

    }

    public SalesContext(DbContextOptions options)
        : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }

    public DbSet<Store> Stores { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public void Seed()
    {
        if (Products.Any() || Customers.Any() || Stores.Any() || Sales.Any())
        {
            return;
        }

        var random = new Random();

        var products = new List<Product>();
        for (int i = 0; i < 50; i++)
        {
            products.Add(new Product
            {
                Name = $"Product {i + 1}",
                Quantity = Math.Round(random.NextDouble() * 100, 2),
                Price = Math.Round((decimal)(random.NextDouble() * 1000), 2)
            });
        }

        var customers = new List<Customer>();
        for (int i = 0; i < 30; i++)
        {
            customers.Add(new Customer
            {
                Name = $"Customer {i + 1}",
                Email = $"customer{i + 1}@example.com",
                CreditCardNumber = $"{random.Next(1000, 9999)}-{random.Next(1000, 9999)}-{random.Next(1000, 9999)}-{random.Next(1000, 9999)}"
            });
        }

        var stores = new List<Store>();
        for (int i = 0; i < 10; i++)
        {
            stores.Add(new Store
            {
                Name = $"Store {i + 1}"
            });
        }

        var sales = new List<Sale>();
        for (int i = 0; i < 100; i++)
        {
            sales.Add(new Sale
            {
                Date = DateTime.Now.AddDays(-random.Next(1, 1000)),
                Product = products[random.Next(products.Count)],
                Customer = customers[random.Next(customers.Count)],
                Store = stores[random.Next(stores.Count)]
            });
        }

        Products.AddRange(products);
        Customers.AddRange(customers);
        Stores.AddRange(stores);
        Sales.AddRange(sales);

        SaveChanges();
    }
}