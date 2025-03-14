using BookStore.models;
using BookStore.Models;

namespace BookStore.Services
{
    public interface ICartService
    {
        List<CartItemView> GetCartItems();

        double GetTotalPrice();

        void AddBook(int bookId);
    }
}
