using Microsoft.Extensions.DependencyInjection;

namespace BookStore
{
    /// <summary>
    /// initialization data
    /// </summary>
    public static class StarupInit
    {
        public static void Init(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Repository.BaseDbContext>();
                SeedDatabase(dbContext);
            }
        }

        public static void SeedDatabase(Repository.BaseDbContext dbContext)
        {
            dbContext.Books.AddRange(new List<Entities.Book>
            {
                new Entities.Book{ Title="book1",Author="author1",Price=10d,Category="category1"},
                new Entities.Book{ Title="book2",Author="author2",Price=11d,Category="category2"},
                new Entities.Book{ Title="book3",Author="author3",Price=12d,Category="category3"},
            });

            dbContext.User.AddRange(new List<Entities.User>
            {
                new Entities.User{ Id=1,Name="admin",Password="123123",Role="admin"},
                new Entities.User{ Id=2,Name="lam",Password="123123",Role=""},
            });

            dbContext.SaveChanges();
        }
    }
}
