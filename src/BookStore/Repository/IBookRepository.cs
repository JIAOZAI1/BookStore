using BookStore.Entities;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        Book? GetBook(int id);

        List<Book> GetAllBooks();

        void AddBook(Book book);

        void UpdateBook(Book book);

        void RemoveBook(int id);
    }
}
