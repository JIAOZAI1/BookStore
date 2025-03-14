using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BaseDbContext context;

        public BookRepository(BaseDbContext dbContext)
        {
            this.context = dbContext;
        }

        public void AddBook(Entities.Book book)
        {
            this.context.Books.Add(book);
            this.context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            this.context.Books.Update(book);
            this.context.SaveChanges();
        }

        public List<Entities.Book> GetAllBooks()
        {
            return context.Books.AsNoTracking().ToList();
        }

        public Entities.Book? GetBook(int id)
        {
            return this.context.Books.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void RemoveBook(int id)
        {
            var bookEntity = this.context.Books.Find(id);
            if (bookEntity == null)
            {
                return;
            }

            this.context.Books.Remove(bookEntity);
            this.context.SaveChanges();
        }
    }
}
