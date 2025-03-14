using BookStore.models;
using BookStore.Repository;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private IBookRepository bookRepository;

        public BookService(IBookRepository _bookRepository)
        {
            this.bookRepository = _bookRepository;
        }

        public void AddBook(Book book)
        {
            var bookEntity = this.bookRepository.GetBook(book.Id);
            if (bookEntity != null)
            {
                this.bookRepository.UpdateBook(new Entities.Book
                {
                    Id = bookEntity.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Price = book.Price,
                    Category = book.Category
                });
                return;
            }

            this.bookRepository.AddBook(new Entities.Book
            {
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                Category = book.Category
            });
        }

        public List<Book> GetAllBooks()
        {
            var books = this.bookRepository.GetAllBooks();
            if (books == null || books.Count == 0)
            {
                return new List<Book>();
            }

            var result = new List<Book>();
            foreach (var bookEntity in books)
            {
                result.Add(new Book
                {
                    Id = bookEntity.Id,
                    Title = bookEntity.Title,
                    Author = bookEntity.Author,
                    Price = bookEntity.Price,
                    Category = bookEntity.Category
                });
            }

            return result;
        }

        public Book GetBook(int id)
        {
            var bookEntity = this.bookRepository.GetBook(id);
            if (bookEntity == null)
            {
                return new Book();
            }

            return new Book
            {
                Id = id,
                Title = bookEntity.Title,
                Author = bookEntity.Author,
                Price = bookEntity.Price,
                Category = bookEntity.Category
            };
        }

        public void RemoveBook(int id)
        {
            this.bookRepository.RemoveBook(id);
        }
    }
}
