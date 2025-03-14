using BookStore.Entities;
using BookStore.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookStore.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly DbSet<User> user;

        public UserService(
            BaseDbContext context,
            IHttpContextAccessor _httpContextAccessor)
        {
            this.httpContextAccessor = _httpContextAccessor;
            this.user = context.User;
        }

        public int GetUserId()
        {
            var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue("userId") ?? string.Empty;
            if (int.TryParse(userId, out var userIdValue))
            {
                return userIdValue;
            }

            return 0;
        }

        public bool TryGetUserId(out int userId)
        {
            userId = GetUserId();
            return userId != 0;
        }

        public bool TryGetUser(string userName, string password, out User? user)
        {
            user = null;
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            user = this.user.FirstOrDefault(i => userName.Equals(i.Name) && password.Equals(i.Password));
            if (user == null)
            {
                return false;
            }

            return true;
            throw new NotImplementedException();
        }
    }
}
