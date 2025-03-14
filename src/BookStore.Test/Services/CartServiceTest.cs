using BookStore.Repository;
using BookStore.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Test.Services
{
    public class CartServiceTest
    {
        private readonly Mock<IUserService> userService;
        private readonly Mock<IBookRepository> bookRepository;
        private readonly Mock<ICartRepository> cartRepository;
        private ICartRepository cartRepository2;
        private readonly DbContextOptionsBuilder<BaseDbContext> dbContextOptionsBuilder;
        private BaseDbContext context;

        public CartServiceTest()
        {
            userService = new Mock<IUserService>();
            bookRepository = new Mock<IBookRepository>();
            cartRepository = new Mock<ICartRepository>();
            dbContextOptionsBuilder = new DbContextOptionsBuilder<BaseDbContext>()
                         .UseInMemoryDatabase(databaseName: "TestDatabase");
            Init();
        }

        private void Init()
        {
            context = new BaseDbContext(dbContextOptionsBuilder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var book1 = new Entities.Book { Id = 1, Title = "Book 1", Author = "Author1", Price = 1, Category = "Category1" };
            var book2 = new Entities.Book { Id = 2, Title = "Book 2", Author = "Author2", Price = 1, Category = "Category2" };
            var book3 = new Entities.Book { Id = 3, Title = "Book 3", Author = "Author3", Price = 1, Category = "Category3" };
            var book4 = new Entities.Book { Id = 4, Title = "Book 4", Author = "Author4", Price = 1, Category = "Category4" };
            var book5 = new Entities.Book { Id = 5, Title = "Book 5", Author = "Author5", Price = 1, Category = "Category5" };
            var book6 = new Entities.Book { Id = 6, Title = "Book 6", Author = "Author6", Price = 1, Category = "Category6" };
            context.Books.Add(book1);
            context.Books.Add(book2);
            context.Books.Add(book3);

            context.Cart.Add(new Entities.Cart { Id = 1, UserId = 1 });

            context.CartItem.Add(new Entities.CartItem { CartId = 1, Book = book4, Quantity = 1 });
            context.CartItem.Add(new Entities.CartItem { CartId = 1, Book = book5, Quantity = 1 });
            context.CartItem.Add(new Entities.CartItem { CartId = 1, Book = book6, Quantity = 1 });
            context.SaveChanges();

            this.cartRepository2 = new CartRepository(context);
        }

        [Fact]
        public void GetCartItems_ShouldReturnCartItems()
        {
            userService.Setup(i => i.GetUserId()).Returns(1);

            var cartService = new CartService(userService.Object, bookRepository.Object, context, cartRepository2);
            var result1 = cartService.GetCartItems();
            Assert.NotNull(result1);
            Assert.Equal(3, result1.Count);
        }

        [Fact]
        public void GetTotalPrice_ShouldReturnDouble()
        {
            userService.Setup(i => i.GetUserId()).Returns(1);

            var cartService = new CartService(userService.Object, bookRepository.Object, context, cartRepository2);
            var result1 = cartService.GetTotalPrice();
            Assert.Equal(3, result1);
        }
    }
}
