namespace BookStore.Models
{
    public class CartItemView
    {
        public int BookId { get; set; }

        public string Title { get; set; } = string.Empty;

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
