using System;
using Domain.Entities;

namespace Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; } 
        public Book() { }

        
        public Book(Guid id, string title, Guid authorId)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            AuthorId = authorId;
        }
    }
}
