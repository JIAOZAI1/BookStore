using BookStore.Repository;
using BookStore.Services;
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
        private readonly Mock<BaseDbContext> context;
        private readonly CartService cartService;

        public CartServiceTest()
        {
            userService = new Mock<IUserService>();
            bookRepository = new Mock<IBookRepository>();
            cartRepository = new Mock<ICartRepository>();
            context = new Mock<BaseDbContext>();
            cartService = new CartService(userService.Object, bookRepository.Object, context.Object, cartRepository.Object);
        }

        [Fact]
        public void GetCartItems_ShouldReturnCartItems()
        {
            userService.Setup(i => i.GetUserId()).Returns(1);
            cartRepository.Setup(i => i.CheckUserCart(1)).Returns(new Entities.Cart());
            cartRepository.Setup(i => i.GetCartItems(1)).Returns(new List<Entities.CartItem>()
            {
                new Entities.CartItem(),
                new Entities.CartItem(),
                new Entities.CartItem(),
            });

            var result1 = cartService.GetCartItems();
            Assert.NotNull(result1);
            Assert.Equal(3, result1.Count);
        }

        [Fact]
        public void GetTotalPrice_ShouldReturnDouble()
        {
            userService.Setup(i => i.GetUserId()).Returns(1);
            cartRepository.Setup(i => i.CheckUserCart(1)).Returns(new Entities.Cart());
            cartRepository.Setup(i => i.GetTotalPrice(1)).Returns(10);

            var result1 = cartService.GetTotalPrice();
            Assert.Equal(10, result1);
        }
    }
}
