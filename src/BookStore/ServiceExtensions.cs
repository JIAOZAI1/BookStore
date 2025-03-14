using BookStore.Repository;
using BookStore.Services;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using BookStore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace BookStore
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // register BAL service
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserService, UserService>();
            services.AddHttpContextAccessor();
            services.AddScoped<ICartService, CartService>();

            // register DAL service
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ICartRepository, CartRepository>();

            // Adding an in-memory database service
            services.AddDbContext<BaseDbContext>(options =>
                options.UseInMemoryDatabase("BookStoreDatabase").LogTo(Console.WriteLine, LogLevel.Information));

        

            services.AddAuthorization();

            return services;
        }
    }
}
