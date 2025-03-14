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
    public class BookServiceTest
    {
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly IBookService bookService;

        public BookServiceTest()
        {
            _mockBookRepository = new Mock<IBookRepository>();
            bookService = new BookService(_mockBookRepository.Object);
        }

        [Fact]
        public void GetBook_ShouldReturnBook()
        {
            _mockBookRepository.Setup(repo => repo.GetBook(1)).Returns(new Entities.Book { Id = 1, Title = "xxx", Author = "xxx", Category = "xxx", Price = 10 });
            var result1 = bookService.GetBook(1);

            Assert.NotNull(result1);
            Assert.Equal(1, result1.Id);
            Assert.Equal("xxx", result1.Title);

            _mockBookRepository.Setup(repo => repo.GetBook(0)).Returns(new Entities.Book());
            var result2 = bookService.GetBook(0);
            Assert.NotNull(result2);
            Assert.Equal(0, result2.Id);
            Assert.Equal(string.Empty, result2.Title);
        }

        [Fact]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            _mockBookRepository.Setup(repo => repo.GetAllBooks()).Returns(new List<Entities.Book>
            {
                new Entities.Book {Id=1,Title="xxx1",Author="aaa1",Category="ccc1",Price=100 },
                new Entities.Book {Id=2,Title="xxx2",Author="aaa2",Category="ccc2",Price=200 },
                new Entities.Book {Id=3,Title="xxx3",Author="aaa3",Category="ccc3",Price=200 },
            });

            var result1 = bookService.GetAllBooks();
            Assert.NotNull(result1);
            Assert.Equal(3, result1.Count);

            _mockBookRepository.Setup(repo => repo.GetAllBooks()).Returns(new List<Entities.Book>());

            var result2 = bookService.GetAllBooks();
            Assert.NotNull(result2);
            Assert.Empty(result2);
        }
    }
}
