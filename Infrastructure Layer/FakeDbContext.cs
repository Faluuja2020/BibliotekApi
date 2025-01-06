using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class FakeDbContext : DbContext
    {
        // DbSets for Books and Authors
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        // Constructor to pass options to base DbContext class
        public FakeDbContext(DbContextOptions<FakeDbContext> options) : base(options) { }

        // Seed method to add some initial data to the "fake" database
        public static void Seed(FakeDbContext context)
        {
            // Check if Authors already exist, and add initial data if not
            if (!context.Authors.Any())
            {
                var authors = new List<Author>
                {
                    new Author { Id = Guid.NewGuid(), Name = "Author One" },
                    new Author { Id = Guid.NewGuid(), Name = "Author Two" }
                };
                context.Authors.AddRange(authors);
                context.SaveChanges();
            }

            // Check if Books already exist, and add initial data if not
            if (!context.Books.Any())
            {
                var books = new List<Book>
                {
                    new Book { Id = Guid.NewGuid(), Title = "Book One", AuthorId = Guid.NewGuid() },
                    new Book { Id = Guid.NewGuid(), Title = "Book Two", AuthorId = Guid.NewGuid() }
                };
                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }
    }
}
