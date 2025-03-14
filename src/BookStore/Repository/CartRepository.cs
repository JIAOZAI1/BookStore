using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly BaseDbContext context;

        public CartRepository(BaseDbContext context)
        {
            this.context = context;
        }

        public List<CartItem> GetCartItems(int userId)
        {
            return context.CartItem
                            .Where(ci => ci.Cart.UserId == userId)
                            .Include(ci => ci.Book)
                            .ToList();
        }

        public Cart CheckUserCart(int userId)
        {
            var cart = this.context.Cart.AsNoTracking().FirstOrDefault(c => c.UserId == userId);
            if (cart == null)
            {
                this.context.Cart.Add(cart = new Cart()
                {
                    UserId = userId,
                });
                this.context.SaveChanges();
            }

            return cart;
        }

        public double GetTotalPrice(int userId)
        {
            return context.CartItem
                           .Where(ci => ci.Cart.UserId == userId)
                           .Include(ci => ci.Book)
                           .Sum(i => i.Quantity * i.Book.Price);
        }

        public Cart? GetCart(int cartId)
        {
            return this.context.Cart.AsNoTracking().FirstOrDefault(i => i.Id == cartId);
        }

        public Cart? GetCartByUserId(int userId)
        {
            return this.context.Cart.AsNoTracking().FirstOrDefault(i => i.UserId == userId);
        }
    }
}
