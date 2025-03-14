using BookStore.Entities;

namespace BookStore.Repository
{
    public interface ICartRepository
    {
        List<Entities.CartItem> GetCartItems(int userId);

        Cart CheckUserCart(int userId);

        double GetTotalPrice(int userId);

        Cart? GetCart(int cartId);

        Cart? GetCartByUserId(int userId);
    }
}
