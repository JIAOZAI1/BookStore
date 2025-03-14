using BookStore.models;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BookStore.Controllers
{
    [Route("api")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        private IBookService bookService;
        public BookStoreController(IBookService _bookService)
        {
            this.bookService = _bookService;
        }

        /// <summary>
        /// 获取书籍信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Book GetBook(int id)
        {
            return this.bookService.GetBook(id);
        }

        /// <summary>
        /// 获取所有书籍信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("books")]
        public List<Book> GetALLBook()
        {
            return this.bookService.GetAllBooks();
        }

        /// <summary>
        /// 添加书籍
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost("book")]
        [Authorize(Roles ="admin")]
        public IActionResult AddBook(Book book)
        {
            this.bookService.AddBook(book);
            return Ok();
        }

        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult RemoveBook(int id)
        {
            this.bookService.RemoveBook(id);
            return Ok();
        }
    }
}
