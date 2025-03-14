using System.ComponentModel.DataAnnotations;

namespace BookStore.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public double Price { get; set; }

        public string? Category { get; set; }
    }
}
