using Application.Interfaces;
using ApplicstionLayers.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly FakeDbContext _context;

        public AuthorRepository(FakeDbContext context)
        {
            _context = context;
        }

        // Method to get all authors
        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        // Method to get an author by their ID
        public async Task<Author> GetByIdAsync(Guid id)
        {
            return await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        // Method to add a new author
        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        // Method to update an existing author
        public async Task UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }

        // Method to delete an author
        public async Task DeleteAsync(Author author)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}
