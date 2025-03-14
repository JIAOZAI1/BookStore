using BookStore.Entities;

namespace BookStore.Services
{
    public interface IUserService
    {
        int GetUserId();

        bool TryGetUserId(out int userId);

        bool TryGetUser(string userName,string password, out User? user);
    }
}
