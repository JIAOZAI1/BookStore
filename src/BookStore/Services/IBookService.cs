using BookStore.models;

namespace BookStore.Services
{
    public interface IBookService
    {
        Book GetBook(int id);

        List<Book> GetAllBooks();

        void AddBook(Book book);

        void RemoveBook(int id);
    }
}
