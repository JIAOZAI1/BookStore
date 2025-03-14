using BookStore.Entities;
using BookStore.models;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class CartService : ICartService
    {
        private readonly IUserService userService;
        private readonly IBookRepository bookRepository;
        private readonly ICartRepository cartRepository;
        private readonly BaseDbContext context;

        public CartService(
            IUserService _userService,
            IBookRepository _bookRepository,
            BaseDbContext _context,
            ICartRepository _cartRepository)
        {
            this.userService = _userService;
            this.cartRepository = _cartRepository;
            this.bookRepository = _bookRepository;
            this.context = _context;
        }

        public void AddBook(int bookId)
        {
            if (!this.userService.TryGetUserId(out int userId))
            {
                throw new Exception("this action must login first");
            }

            this.cartRepository.CheckUserCart(userId);
            var cart = this.cartRepository.GetCartByUserId(userId);
            var book = this.bookRepository.GetBook(bookId);

            if (cart == null || book == null)
            {
                throw new Exception("cart or book not found");
            }

            var cartItem = context.CartItem.FirstOrDefault(ci => ci.CartId == cart.Id && ci.BookId == book.Id);
            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                var newCartItem = new CartItem
                {
                    CartId = cart.Id,
                    BookId = bookId,
                    Quantity = 1
                };

                context.CartItem.Add(newCartItem);
            }

            context.SaveChanges();
        }

        public List<CartItemView> GetCartItems()
        {
            var userId = this.userService.GetUserId();
            this.cartRepository.CheckUserCart(userId);
            var cartItems = this.cartRepository.GetCartItems(userId);

            var result = new List<CartItemView>();
            foreach (var item in cartItems)
            {
                result.Add(new CartItemView
                {
                    Title = item.Book.Title,
                    Price = item.Book.Price,
                    Quantity = item.Quantity,
                    BookId = item.Book.Id,
                });
            }

            return result;
        }

        public double GetTotalPrice()
        {
            var userId = this.userService.GetUserId();
            this.cartRepository.CheckUserCart(userId);

            return this.cartRepository.GetTotalPrice(userId);
        }
    }
}
