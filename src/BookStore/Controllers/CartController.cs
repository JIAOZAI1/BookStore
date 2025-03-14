using BookStore.models;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartService cartService;

        public CartController(ICartService _cartService)
        {
            this.cartService = _cartService;
        }

        /// <summary>
        /// 获取购物车书籍信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("cart-items")]
        public List<CartItemView> GetBooks()
        {
            return this.cartService.GetCartItems();
        }

        /// <summary>
        /// 计算购物车所以书籍总价格
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("total-price")]
        public double GetTotalPrice()
        {
            return this.cartService.GetTotalPrice();
        }

        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("add-book/{bookId}")]
        public IActionResult AddBook(int bookId)
        {
            this.cartService.AddBook(bookId);
            return this.Ok();
        }
    }
}
