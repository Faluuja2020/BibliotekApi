using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class FakeDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    public FakeDbContext(DbContextOptions<FakeDbContext> options) : base(options) { }

    public static FakeDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<FakeDbContext>()
            .UseInMemoryDatabase(databaseName: "FakeLibraryDb")
            .Options;

        var context = new FakeDbContext(options);

        // Seed data
        FakeDbSeeder.Seed(context);

        return context;
    }
}

public static class FakeDbSeeder
{
    public static void Seed(FakeDbContext context)
    {
        // Add seeding logic here
    }
}