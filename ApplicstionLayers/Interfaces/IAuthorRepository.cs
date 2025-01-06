using System;
using System.Collections.Generic;
using Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicstionLayers.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(Guid id);
        Task AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(Author author);
    }
}
