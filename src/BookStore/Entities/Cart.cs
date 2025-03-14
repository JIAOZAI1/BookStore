using System.ComponentModel.DataAnnotations;

namespace BookStore.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); 
    }
}
