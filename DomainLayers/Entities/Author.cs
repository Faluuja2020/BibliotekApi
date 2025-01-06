using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; } // Navigation property

        public Author()
        {
            Books = new List<Book>();
        }

        public Author(Guid id, string name) : this()
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
