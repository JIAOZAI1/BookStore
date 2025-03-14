namespace BookStore.models
{
    public class Cart
    {
        public int UserId { get; set; }

        public List<int> Books { get; set; } = new List<int>();
    }
}
